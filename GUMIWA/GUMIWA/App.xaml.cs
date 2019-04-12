using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamStorage;

using Unosquare.Labs.EmbedIO;
using Unosquare.Labs.EmbedIO.Modules;

using AIUWMG.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AIUWMG
{
    public partial class App : Application
    {

        public App()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("====== resource debug info =========");
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            System.Diagnostics.Debug.WriteLine("====================================");

#endif
            InitializeComponent();

            // Server must be started, before WebView is initialized,
            // because we have no reload implemented in this sample.
            Task.Factory.StartNew(async () =>
            {
                using (var server = new WebServer("http://*:200501"))
                {
                    // Assembly assembly = typeof(App).Assembly;
                    // server.RegisterModule(new ResourceFilesModule(assembly, "EmbedIO.Forms.Sample.html"));
                    var url = FileSystem.Current.LocalStorage.Path;
                    Debug.WriteLine($"URL={url}");
                    server.RegisterModule(new StaticFilesModule(url));

                    await server.RunAsync();
                }
            });

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
