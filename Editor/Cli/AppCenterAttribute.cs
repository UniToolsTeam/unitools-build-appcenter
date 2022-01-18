using System;

namespace UniTools.CLI
{
    public sealed class AppCenterAttribute : BaseCliToolAttribute
    {
        private const string ToolName = "appcenter";

        public AppCenterAttribute() : base(ToolName)
        {
        }

        public override BaseCliTool Create()
        {
            return new AppCenter(
                PathResolver.Default.Execute(ToolName).Output.Split(Environment.NewLine.ToCharArray())?[0],
                CommandLine.Default);
        }
    }
}