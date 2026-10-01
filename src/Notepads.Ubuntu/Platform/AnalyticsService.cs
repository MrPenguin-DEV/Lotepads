// ---------------------------------------------------------------------------------------------
//  Copyright (c) 2019-2024, Jiaqi (0x7c13) Liu. All rights reserved.
//  See LICENSE file in the project root for license information.
// ---------------------------------------------------------------------------------------------

namespace Notepads.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Maintains the analytics call surface on desktop platforms without
    /// depending on the Microsoft Store-only engagement SDK.
    /// </summary>
    public static class AnalyticsService
    {
        public static void TrackEvent(string eventName, IDictionary<string, string> properties = null)
        {
        }

        public static void TrackError(Exception exception, IDictionary<string, string> properties = null)
        {
        }
    }
}
