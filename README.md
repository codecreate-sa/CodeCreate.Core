# CodeCreate.Core

## GitHub Actions

| Name | Status | GH Action | Description |
|:--|:--|:--|:--|
| Build CodeCreate.Core | [![codecreate-core-ci](https://github.com/codecreate-sa/CodeCreate.Core/actions/workflows/codecreate-core-ci.yml/badge.svg)](https://github.com/codecreate-sa/CodeCreate.Core/actions/workflows/codecreate-core-ci.yml) | [codecreate-core-ci.yml](./.github/workflows/codecreate-core-ci.yml) | Triggered when a new PR is created against the **main** branch. It builds the CodeCreate.Core library and runs its tests. |
| Create CodeCreate.Core library package | [![codecreate-core-cd](https://github.com/codecreate-sa/CodeCreate.Core/actions/workflows/codecreate-core-cd.yml/badge.svg)](https://github.com/codecreate-sa/CodeCreate.Core/actions/workflows/codecreate-core-cd.yml) | [codecreate-core-cd.yml](./.github/workflows/codecreate-core-cd.yml) | Triggered whenever a push is made in the **main** branch (i.e., PR is merged). It builds the CodeCreate.Core, runs its tests, packs the source code into a .nupkg file and finally pushes it to Code Create's GitHub Packages nuget source |