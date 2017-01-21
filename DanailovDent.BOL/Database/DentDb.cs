using DanailovDent.BOL.Pocos;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.MySql;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DanailovDent.BOL
{
    public partial class DentDb : danailovDB
    {
        private static MySqlDataProvider Provider = new MySqlDataProvider();
        private List<Action> actionList = new List<Action>();
        private List<Action> keyList = new List<Action>();

        public DentDb(string connectionString)
            : base(DentDb.Provider, connectionString)
        {
            this.actionList = new List<Action>();
        }

        public override DataConnectionTransaction BeginTransaction()
        {
            this.ClearActions();
            return base.BeginTransaction();
        }

        public override DataConnectionTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel)
        {
            this.ClearActions();
            return base.BeginTransaction(isolationLevel);
        }

        public override void CommitTransaction()
        {
            base.CommitTransaction();
            this.CommitActions();
            this.ClearKeys();
        }

        public override void RollbackTransaction()
        {
            base.RollbackTransaction();
            this.ClearActions();
            this.RollbackKeys();
        }

        public void Delete<T>(params T[] pocos)
        {
            foreach (T p in pocos)
                DataExtensions.Delete(this, p);
        }

        /// <summary>
        /// Добавя код който да се изпълни
        /// </summary>
        /// <param name="action"></param>
        public void AddAction(Action action)
        {
            this.actionList.Add(action);
        }
        private void ClearActions()
        {
            if (this.actionList == null || this.actionList.Count == 0)
                return;

            this.actionList.Clear();
        }
        private void CommitActions()
        {
            if (actionList == null || actionList.Count == 0)
                return;

            foreach (var a in actionList)
                a.Invoke();
        }

        /// <summary>
        /// Запазва оригиналната стойност на дадено поле и при Rollback връща стойността
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        public void AddRollbackKey<T>(T model, string propertyName)
        {
            var prop = model.GetType().GetProperty(propertyName);
            object value = prop.GetValue(model, null);
            this.keyList.Add(() => prop.SetValue(model, value));
        }
        private void ClearKeys()
        {
            if (keyList == null || keyList.Count == 0)
                return;

            keyList.Clear();
        }
        private void RollbackKeys()
        {
            if (keyList == null || keyList.Count == 0)
                return;

            foreach (Action a in keyList)
                a.Invoke();
        }

        /// <summary>
        /// Запазва poco модели от дадения тип
        /// </summary>
        /// <typeparam name="T">Тип на poco моделите</typeparam>
        /// <param name="pocos">Poco модели за запазване</param>
        public void Save<T>(params T[] pocos)
        {
            var primaryKey = DentDb.GetPrimaryKeyProperty<T>();
            var lastUpdateKey = GetLastUpdateProperty<T>();
            foreach (T poco in pocos)
            {
                uint pocoID = Convert.ToUInt32(primaryKey.GetValue(poco, null));

                if (pocoID == 0)
                {
                    //insert
                    pocoID = Convert.ToUInt32(this.InsertWithIdentity<T>(poco));
                    primaryKey.SetValue(poco, pocoID, null);
                }
                else
                {
                    //update
                    if (lastUpdateKey != null)
                        lastUpdateKey.SetValue(poco, DateTime.Now, null);

                    this.Update<T>(poco);
                }
            }
        }

        private static Dictionary<Type, PropertyInfo> PrimaryKeysCache = new Dictionary<Type, PropertyInfo>();
        private static Dictionary<Type, PropertyInfo> LastUpdateKeysCache = new Dictionary<Type, PropertyInfo>();
        private static PropertyInfo GetPrimaryKeyProperty<T>()
        {
            Type type = typeof(T);
            if (!DentDb.PrimaryKeysCache.ContainsKey(type))
            {
                var primKey = type.GetProperties()
                    .Where(m => m.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Count() > 0);

                if (primKey.Count() != 1)
                    throw new ArgumentException("Няма еднозначно описан първичен ключ!");

                var prim = primKey.FirstOrDefault();
                DentDb.PrimaryKeysCache.Add(type, prim);
            }

            return DentDb.PrimaryKeysCache[type];
        }
        private static PropertyInfo GetLastUpdateProperty<T>()
        {
            Type type = typeof(T);
            if (!DentDb.LastUpdateKeysCache.ContainsKey(type))
            {
                var primKey = type.GetProperties()
                    .Where(m => m.Name == "LastUpdateTime")
                    .FirstOrDefault();

                DentDb.LastUpdateKeysCache.Add(type, primKey);
            }

            return DentDb.LastUpdateKeysCache[type];
        }
    }
}
