# Cake.ESLint

[![standard-readme compliant][]][standard-readme]
[![All Contributors][all-contributorsimage]](#contributors)
[![Appveyor build][appveyorimage]][appveyor]
[![Codecov Report][codecovimage]][codecov]
[![NuGet package][nugetimage]][nuget]

Makes ESLint available as a tool in Cake.

Makes [ESLint](https://eslint.org/docs/user-guide/command-line-interface) available as a tool in [Cake](https://cakebuild.net/)

## Table of Contents

- [Install](#install)
- [Usage](#usage)
- [Maintainer](#maintainer)
- [Contributing](#contributing)
  - [Contributors](#contributors)
- [License](#license)

## Install

```cs
#addin nuget:?package=Cake.ESLint
```

## Usage

```cs
#addin nuget:?package=Cake.ESLint

Task("MyTask").Does(() => {
  ESLint();
});
```

## Maintainer

[Nils Andresen @nils-a][maintainer]

## Contributing

Cake.ESLint follows the [Contributor Covenant][contrib-covenant] Code of Conduct.

We accept Pull Requests.
Please see [the contributing file][contributing] for how to contribute to Cake.ESLint.

Small note: If editing the Readme, please conform to the [standard-readme][] specification.

This project follows the [all-contributors][] specification. Contributions of any kind welcome!

### Contributors

Thanks goes to these wonderful people ([emoji key][emoji-key]):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore -->
<!-- ALL-CONTRIBUTORS-LIST:END -->

## License

[MIT License © Nils Andresen][license]

[all-contributors]: https://github.com/all-contributors/all-contributors
[all-contributorsimage]: https://img.shields.io/github/all-contributors/cake-contrib/Cake.ESLint.svg?color=orange&style=flat-square
[appveyor]: https://ci.appveyor.com/project/nilsa/cake-eslint
[appveyorimage]: https://img.shields.io/appveyor/ci/nilsa/cake-eslint.svg?logo=appveyor&style=flat-square
[codecov]: https://codecov.io/gh/cake-contrib/Cake.ESLint
[codecovimage]: https://img.shields.io/codecov/c/github/cake-contrib/Cake.ESLint.svg?logo=codecov&style=flat-square
[contrib-covenant]: https://www.contributor-covenant.org/version/1/4/code-of-conduct
[contributing]: CONTRIBUTING.md
[emoji-key]: https://allcontributors.org/docs/en/emoji-key
[maintainer]: https://github.com/nils-a
[nuget]: https://nuget.org/packages/Cake.ESLint
[nugetimage]: https://img.shields.io/nuget/v/Cake.ESLint.svg?logo=nuget&style=flat-square
[license]: LICENSE.txt
[standard-readme]: https://github.com/RichardLitt/standard-readme
[standard-readme compliant]: https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square
