Param
(
    [Parameter(Mandatory=$True,Position=1)]
    [string] $TargetAssembly,

    [Parameter(Mandatory=$True,Position=2)]
    [string] $TargetTestAssembly
)

$openCoverExe = ".\packages\OpenCover.4.7.922\tools\OpenCover.Console.exe"
$nUnitRunnerExe = ".\packages\NUnit.ConsoleRunner.3.12.0\tools\nunit3-console.exe"
$reportGeneratorExe = ".\packages\ReportGenerator.4.8.7\tools\net47\ReportGenerator.exe"

## OpenCover to consume results of executing tests
&$openCoverExe -target:$nUnitRunnerExe ("-targetargs:{0}" -f $TargetTestAssembly) -register:user -filter:+[$TargetAssembly]*-[*Test]*

## Generate Test Coverage report from OpenCover result XML file
&$reportGeneratorExe -reports:results.xml -targetdir:coverage