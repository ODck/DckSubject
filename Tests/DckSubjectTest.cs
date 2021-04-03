using DckSubject;
using Xunit;

namespace Tests
{
    public class DckSubjectTest
    {
        [Fact]
        public void TestDckSubjectTrigger()
        {
            var testInt = 0;
            var subject = new DckSubject<int>();
            var connection = subject.Subscribe(x => testInt = x);
            subject.Trigger(1);
            Assert.Equal(1, testInt);
        }

        [Fact]
        public void TestSubjectConnections()
        {
            var testInt = 0;
            var subject = new DckSubject<int>();
            var connection = subject.Connect(x => testInt += x);
            Assert.Equal(0, testInt);
            subject.Trigger(1);
            Assert.Equal(1, testInt);
            connection.Disconnect();
            subject.Trigger(1);
            Assert.Equal(1, testInt);
            connection.Reconnect();
            subject.Trigger(1);
            Assert.Equal(2, testInt);
        }
    }
}