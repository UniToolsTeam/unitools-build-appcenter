# UniTools Build AppCenter
Build steps to distribute the artifact to the AppCenter

# Features
- Deploy mobile builds (.ipa and .apk) to the App Center application using [AppCenter CLI](https://docs.microsoft.com/en-us/appcenter/cli/)

# Dependencies
- [UniTools CLI](https://github.com/UniToolsTeam/unitools-cli)
- [UniTools IO](https://github.com/UniToolsTeam/unitools-io)
- [UniTools Build](https://github.com/UniToolsTeam/unitools-build)

# Installation
Install [AppCenter CLI](https://docs.microsoft.com/en-us/appcenter/cli/)

### Download
[Latest Releases](../../releases/latest)

### Unity Package Manager (UPM)

> You will need to have git installed and set in your system PATH.
> Check package [dependencies](https://github.com/UniToolsTeam/unitools-build-ios/blob/master/package.json)

Add the following to `Packages/manifest.json` where x.x.x the version (tag) check [Latest Releases](../../releases/latest):

```
{
  "dependencies": {
    "com.unitools.cli": "https://github.com/UniToolsTeam/unitools-cli.git#x.x.x",
    "com.unitools.io": "https://github.com/UniToolsTeam/unitools-io.git#x.x.x",
    "com.unitools.build": "https://github.com/UniToolsTeam/unitools-build.git#x.x.x",
    "com.unitools.appcenter": "https://github.com/UniToolsTeam/unitools-build-appcenter.git#x.x.x",
    "...": "..."
  }
}
```

# Getting Started
In progress..
