using OwnApt.Common.Dto;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Api.Tests.Component
{
    public class EquatableTest
    {
        #region Methods

        [Fact]
        public void CanEquateModels()
        {
            var assemblyName = new AssemblyName(nameof(Api));
            var classTypes = Assembly.Load(assemblyName).GetTypes().Where(t => t != typeof(OwnApt.Common.Dto.Equatable) && typeof(Equatable).IsAssignableFrom(t));

            foreach (var type in classTypes)
            {
                var orig = Activator.CreateInstance(type);
                var copy = Activator.CreateInstance(type);
                Assert.Equal(orig, copy);
            }
        }

        #endregion Methods
    }
}
