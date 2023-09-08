#!/bin/bash

MOD_NAME="CompactWorkTab"

# Repository variables

gh variable set MOD_NAME -b $MOD_NAME
gh variable set SLN_PATH -b "Source/Assemblies/$MOD_NAME.sln"
gh variable set CSPROJ_PATH -b "Source/Assemblies/$MOD_NAME/$MOD_NAME.csproj"
gh variable set ASSEMBLY_PATH -b "1.4/Assemblies/$MOD_NAME.dll"
gh variable set ZIP_CONTENTS -b "1.4/ About/ Textures/ CHANGELOG.txt LICENSE"

# Pull request type labels

gh label create "chore" -d "A change that doesn't affect production code" -c "#c5def5" -f
gh label create "bug" -d "Something isn't working" -c "#d73a4a" -f
gh label create "enhancement" -d "Improvement to an existing feature" -c "#a2eeef" -f
gh label create "feature" -d "New feature" -c "#a2eeef" -f
gh label create "release" -d "New release" -c "#d93f0b" -f
