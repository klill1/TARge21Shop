using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;

namespace TARge21Shop.SpaceshipTest.Mock
{
    public class MockIHostEnviroment : IHostEnvironment
    {
        public string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContentRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string EnvironmentName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
