# Cake.ESLint

[![standard-readme compliant][]][standard-readme]
[![All Contributors][all-contributorsimage]](#contributors)
[![Build][githubimage]][githubbuild]
[![Codecov Report][codecovimage]][codecov]
[![NuGet package][nugetimage]][nuget]

Makes ESLint available as a tool in Cake.

Makes [ESLint](https://eslint.org/docs/user-guide/command-line-interface) available as a tool in [Cake](https://cakebuild.net/)

## Table of Contents

- [Install](#install)
- [Usage](#usage)
- [Discussion](#discussion)
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

## Discussion

If you have questions, search for an existing one, or create a new discussion on the Cake GitHub repository, using the `extension Q&A` category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

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
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tr>
    <td align="center"><a href="https://github.com/nils-a"><img src="https://avatars.githubusercontent.com/u/349188?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Nils Andresen</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.ESLint/commits?author=nils-a" title="Code">💻</a> <a href="#maintenance-nils-a" title="Maintenance">🚧</a></td>
  </tr>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

## License

[MIT License © Nils Andresen][license]

[all-contributors]: https://github.com/all-contributors/all-contributors
[all-contributorsimage]: https://img.shields.io/github/all-contributors/cake-contrib/Cake.ESLint.svg?color=orange&style=flat-square
[githubbuild]: https://github.com/cake-contrib/Cake.ESLint/actions/workflows/build.yml?query=branch%3Adevelop
[githubimage]: https://github.com/cake-contrib/Cake.ESLint/actions/workflows/build.yml/badge.svg?branch=develop
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
