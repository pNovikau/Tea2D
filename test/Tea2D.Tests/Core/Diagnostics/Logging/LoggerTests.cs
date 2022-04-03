using System;
using System.IO;
using AutoFixture;
using FluentAssertions;
using Tea2D.Core.Diagnostics.Logging;
using Xunit;

namespace Tea2D.Tests.Core.Diagnostics.Logging
{
    public class LoggerTests
    {
        private static readonly Fixture Fixture = new();

        private readonly ILogger _underTests = Logger.Instance;

        [Fact]
        public void LogTrace_Should_Log_Trace_Message()
        {
            var message = Fixture.Create<string>();
            using var outStream = new MemoryStream(10000);
            using var streamWriter = TextWriter.Synchronized(new StreamWriter(outStream));

            using (MockOutStream(streamWriter))
            {
                _underTests.LogTrace(message);
            }

            streamWriter.Flush();

            ReadFromStream(outStream).Should().Be($"[Trace]   {message}\r\n");
        }

        [Fact]
        public void LogDebug_Should_Log_Debug_Message()
        {
            var message = Fixture.Create<string>();
            using var outStream = new MemoryStream(10000);
            using var streamWriter = TextWriter.Synchronized(new StreamWriter(outStream));

            using (MockOutStream(streamWriter))
            {
                _underTests.LogDebug(message);
            }

            streamWriter.Flush();

            ReadFromStream(outStream).Should().Be($"[Debug]   {message}\r\n");
        }

        [Fact]
        public void LogInfo_Should_Log_Info_Message()
        {
            var message = Fixture.Create<string>();
            using var outStream = new MemoryStream(10000);
            using var streamWriter = TextWriter.Synchronized(new StreamWriter(outStream));

            using (MockOutStream(streamWriter))
            {
                _underTests.LogInfo(message);
            }

            streamWriter.Flush();

            ReadFromStream(outStream).Should().Be($"[Info]    {message}\r\n");
        }

        [Fact]
        public void LogWarning_Should_Log_Warning_Message()
        {
            var message = Fixture.Create<string>();
            using var outStream = new MemoryStream(10000);
            using var streamWriter = TextWriter.Synchronized(new StreamWriter(outStream));

            using (MockOutStream(streamWriter))
            {
                _underTests.LogWarning(message);
            }

            streamWriter.Flush();

            ReadFromStream(outStream).Should().Be($"[Warning] {message}\r\n");
        }

        [Fact]
        public void LogError_Should_Log_Error_Message()
        {
            var message = Fixture.Create<string>();
            using var outStream = new MemoryStream(10000);
            using var streamWriter = TextWriter.Synchronized(new StreamWriter(outStream));

            using (MockOutStream(streamWriter))
            {
                _underTests.LogError(message);
            }

            streamWriter.Flush();

            ReadFromStream(outStream).Should().Be($"[Error]   {message}\r\n");
        }

        [Fact]
        public void LogFatal_Should_Log_Fatal_Message()
        {
            var message = Fixture.Create<string>();
            using var outStream = new MemoryStream(10000);
            using var streamWriter = TextWriter.Synchronized(new StreamWriter(outStream));

            using (MockOutStream(streamWriter))
            {
                _underTests.LogFatal(message);
            }

            streamWriter.Flush();

            ReadFromStream(outStream).Should().Be($"[Fatal]   {message}\r\n");
        }

        private static string ReadFromStream(Stream stream)
        {
            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            return streamReader.ReadToEnd();
        }

        private static IDisposable MockOutStream(TextWriter outStream)
        {
            var defaultStream = Console.Out;

            Console.SetOut(outStream);

            return new DisposableAction(() => Console.SetOut(defaultStream));
        }

        private class DisposableAction : IDisposable
        {
            private readonly Action _action;

            public DisposableAction(Action action)
            {
                _action = action;
            }

            public void Dispose()
            {
                _action.Invoke();
                GC.SuppressFinalize(this);
            }
        }
    }
}