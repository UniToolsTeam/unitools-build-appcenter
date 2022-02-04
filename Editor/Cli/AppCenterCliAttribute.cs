using System;

namespace UniTools.Build
{
    public sealed class AppCenterCliAttribute : BaseCliToolAttribute
    {
        private const string ToolName = "appcenter";

        public AppCenterCliAttribute() : base(ToolName)
        {
        }

        public override BaseCliTool Create()
        {
            return new AppCenterCli(
                PathResolver.Default.Execute(ToolName).Output.Split(Environment.NewLine.ToCharArray())?[0],
                CommandLine.Default);
        }
    }
}