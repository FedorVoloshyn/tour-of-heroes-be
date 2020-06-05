using System;

namespace TourOfHeroes.Services
{
    /// <summary>
    /// Attribute used for automatic type registration
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        public bool AsSelf { get; set; }
        public bool AsSingleton { get; set; }
    }
}