﻿using Tea2D.Trace.ViewModels.Samples.Tabs;

namespace Tea2D.Trace.ViewModels.Samples;

public sealed class MainWindowViewModelSample : MainWindowViewModel
{
    public MainWindowViewModelSample() : base(new MetricsTabViewModelSample())
    {
    }
}