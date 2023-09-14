#!/bin/bash

# Pull request type labels

gh label create "chore" -d "A change that doesn't affect production code" -c "#c5def5" -f
gh label create "bug" -d "Something isn't working" -c "#d73a4a" -f
gh label create "enhancement" -d "Improvement to an existing feature" -c "#a2eeef" -f
gh label create "feature" -d "New feature" -c "#a2eeef" -f
gh label create "release" -d "New release" -c "#d93f0b" -f
