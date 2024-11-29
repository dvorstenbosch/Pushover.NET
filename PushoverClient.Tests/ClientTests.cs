using FluentAssertions;

namespace PushoverClient.Tests
{
    public class ClientTests
    {
        private const string TEST_APP_KEY = "YOURAPPKEY";
        private const string TEST_USER_KEY = "YOURUSERKEY";

        [Fact]
        public void PushWithValidParms_ReturnsSuccessful()
        {
            //  Arrange
            string title = "Test title";
            string message = "This is a test push notification message";

            //  Act
            Pushover pclient = new Pushover(TEST_APP_KEY);
            PushResponse response = pclient.Push(title, message, TEST_USER_KEY);

            //  Assert
            response.Should().NotBeNull();
            response.Status.Should().Be(1);
        }

        [Fact]
        public async Task PushAsyncWithValidParms_ReturnsSuccessful()
        {
            //  Arrange
            string title = "Test title";
            string message = "This is a test push notification message";

            //  Act
            Pushover pclient = new Pushover(TEST_APP_KEY);
            PushResponse response = await pclient.PushAsync(title, message, TEST_USER_KEY);

            //  Assert
            response.Should().NotBeNull();
            response.Status.Should().Be(1);
        }

        [Fact]
        public async Task PushWithNoKey_ReturnsError()
        {
            //  Arrange
            string title = "Test title";
            string message = "This is a test push notification message";

            //  Act
            Pushover pclient = new Pushover(TEST_APP_KEY);
            Func<Task<PushResponse>> action = async () => await pclient.PushAsync(title, message);

            //  Assert - above code should error before this
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task PushWithDefaultKey_ReturnsSuccessful()
        {
            //  Arrange
            string title = "Test title";
            string message = "This is a test push notification message";

            //  Act
            Pushover pclient = new Pushover(TEST_APP_KEY) { DefaultUserGroupSendKey = TEST_USER_KEY };
            PushResponse response = await pclient.PushAsync(title, message);

            //  Assert
            response.Should().NotBeNull();
            response.Status.Should().Be(1);
        }

        [Fact]
        public async Task PushWithHighPriority_ReturnsSuccessful()
        {
            //  Arrange
            string title = "Test title";
            string message = "This is a test push notification message";
            Priority priority = Priority.High;

            //  Act
            Pushover pclient = new Pushover(TEST_APP_KEY) { DefaultUserGroupSendKey = TEST_USER_KEY };
            PushResponse response = await pclient.PushAsync(title, message, priority: priority);

            //  Assert
            response.Should().NotBeNull();
            response.Status.Should().Be(1);
        }

        [Fact]
        public async Task PushWithEmergencyPriority_ReturnsSuccessful()
        {
            // Arrange
            string title = "Test title";
            string message = "This is a test push notification message";
            Priority priority = Priority.Emergency;

            //  Act
            Pushover pclient = new Pushover(TEST_APP_KEY) { DefaultUserGroupSendKey = TEST_USER_KEY };
            PushResponse response = await pclient.PushAsync(title, message, priority: priority);

            //  Assert
            response.Should().NotBeNull();
            response.Status.Should().Be(1);
        }

        [Fact]
        public async Task PushWithSound_ReturnsSuccessful()
        {
            //  Arrange
            string title = "Test title";
            string message = "This is a test push notification message";

            //  Act
            Pushover pclient = new Pushover(TEST_APP_KEY) { DefaultUserGroupSendKey = TEST_USER_KEY };
            PushResponse response = await pclient.PushAsync(title, message, notificationSound: NotificationSound.Alien);

            //  Assert
            response.Should().NotBeNull();
            response.Status.Should().Be(1);
        }
    }
}