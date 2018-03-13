﻿using System;

namespace WtsXPlat.Mobile.ViewModels.Navigation
{
    public class MasterDetailPageMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }
        public string IconSource { get; set; }
    }
}
