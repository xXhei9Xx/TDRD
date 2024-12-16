rem needs project dirname migration
rem uses unity from defined path enviroment
rem based on article from documentation
rem https://docs.unity3d.com/6000.0/Documentation/Manual/embedded-linux-build-command-line.html
rem https://docs.unity3d.com/6000.0/Documentation/Manual/EditorCommandLineArguments.html
rem https://docs.unity3d.com/6000.0/Documentation/Manual/PlatformSpecific.html

rem related to topic: https://fadhilnoer.medium.com/automating-unity-builds-part-1-ba0c60e8d06b

Unity -quit -batchmode -nographics -buildTarget StandaloneWindows -executeMethod Builder.Build -projectPath ./project-files