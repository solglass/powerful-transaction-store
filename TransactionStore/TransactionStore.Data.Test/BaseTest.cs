﻿using TransactionStore.Core.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseTest
    {
        private string _pathToConfig;
        protected IOptions<AppSettings> _options;

        public BaseTest()
        {
            _pathToConfig = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf(".Test"));
            _options = TestHelper.GetIOptionsFromAppSettings(_pathToConfig);
        }
    }
}
