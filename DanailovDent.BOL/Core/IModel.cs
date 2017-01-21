﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DanailovDent.BOL
{
    public interface IModel : INotifyPropertyChanged
    {
        bool IsChanged { get; set; }
    }
}
