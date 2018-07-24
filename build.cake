#addin nuget:?package=Cake.Unity3D
#addin nuget:?package=SharpZipLib
#addin nuget:?package=Cake.Compression

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var unityVersion = "Unity 2018.2.0f2 (64-bit)";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
.Does(() => {
   Information("Hello Cake!");
});

Task("Clean")
.Does(() => {
    if(DirectoryExists("./Build"))
    {
        CleanDirectory("./Build");
    }
    else
    {
        CreateDirectory("./Build");
    }

	if(FileExists("./Build.zip"))
	{
		DeleteFile("./Build.zip");
	}
});

Task("Build")
.IsDependentOn("Clean")
.Does(() => {
    // Presuming the build.cake file is within the Unity3D project folder.
	var projectPath = System.IO.Path.GetFullPath("./");
	
	// The location we want the build application to go
	var outputPath = System.IO.Path.Combine(projectPath, "Build", "VnotifieR.exe");
	
	// Get the absolute path to the 2018.1.0f1 Unity3D editor.
	string unityEditorLocation;
	if (!TryGetUnityInstall(unityVersion, out unityEditorLocation)) 
	{
		Error(string.Format("Failed to find '{0}' install location", unityVersion));
		return;
	}
	
	// Create our build options.
	var options = new Unity3DBuildOptions()
	{
		Platform = Unity3DBuildPlatform.StandaloneWindows64,
		OutputPath = outputPath,
		UnityEditorLocation = unityEditorLocation,
		ForceScriptInstall = true,
		BuildVersion = "1.0.0"
	};
	
	// Perform the Unity3d build.
	BuildUnity3DProject(projectPath, options);
});

Task("CopyBuildAssets")
.Does(() => {
	CopyFiles(GetFiles("./BuildAssets/*"), "./Build");
});

Task("CopyLicenses")
.Does(() => {
	CopyFiles(GetFiles("./Licenses/*"), "./Build");
});

Task("Copy")
.IsDependentOn("CopyBuildAssets")
.IsDependentOn("CopyLicenses");

Task("Pack")
.IsDependentOn("Build")
.IsDependentOn("Copy")
.Does(() => {
	Zip("./Build", "./Build.zip");
});

RunTarget(target);