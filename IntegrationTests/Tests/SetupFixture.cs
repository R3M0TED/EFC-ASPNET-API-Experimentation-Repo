namespace IntegrationTests.Tests
{
    [SetUpFixture] 
    public class SetupFixture
    {
        private static ApiWebApplicationFactory<Program> Factory { get; set; } = null!;
        internal static HttpClient Client { get; private set; } = null!;

        [OneTimeSetUp]
        public static void GlobalSetup()
        {
            Factory = new ApiWebApplicationFactory<Program>();
            Client = Factory.CreateClient(); 
        }

        [OneTimeTearDown]
        public static void GlobalCleanup()
        {
            Client.Dispose();
            Factory.CleanUp();
            Factory.Dispose();
        }
    }
}
