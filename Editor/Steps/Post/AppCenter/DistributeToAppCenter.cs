using System.Threading.Tasks;
using UniTools.Build;
using UnityEngine;

namespace UniTools.Build.AppCenter
{
    [CreateAssetMenu(
        fileName = nameof(DistributeToAppCenter),
        menuName = MenuPaths.Steps + nameof(AppCenter) + "/" + nameof(DistributeToAppCenter)
    )]
    public sealed class DistributeToAppCenter : BuildStep
    {
        [SerializeField] private string m_apiToken = default;
        [SerializeField] private string m_appName = default;
        [SerializeField] private string m_group = "Collaborators";
        [SerializeField] private PathProperty m_builtFilePath = default;

        public override async Task Execute()
        {
            await Task.CompletedTask;
            string command = $"distribute release " +
                $" --app {m_appName}" +
                $" --file {m_builtFilePath.ToString()}" +
#if UNITY_EDITOR_WIN
                $" --group {m_group}" +
#elif UNITY_EDITOR_OSX
                $" --group \"{m_group}\"" +
#else
                throw new Exception($"{nameof(DistributeToAppCenter)}: {m_group} not adjusted!");
#endif

                $" --build-version {Application.version}" +
                $" --token {m_apiToken}";

            ToolResult result = Cli.Tool<AppCenterCli>().Execute(command, ProjectPath.Value);

            if (result.ExitCode != 0)
            {
                throw new BuildStepFailedException($"{nameof(DistributeToAppCenter)}: Failed! {result.ToString()}");
            }
        }
    }
}