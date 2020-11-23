# Active-LT: Stateless Behavior Trees for Unity

This package offers stateless behavior trees for Unity, and is a light version
of the [Active Logic](https://assetstore.unity.com/packages/tools/ai/active-logic-151850)
asset available from the Unity Asset Store.

## Who should use this?

This package is recommended for all prospective Active Logic users. As a [strong proponent of
stateless control](https://www.gamasutra.com/blogs/ThibaudDeSouza/20201012/371528/Behavior_trees_and_the_future_of_intelligent_control.php), I encourage users to get familiar with the stateless model before getting
started with ordered composites and decorators.

Stateful control offers added convenience, also helping with problems that stateless control
handles less gracefully (such as related to narrative vs logical behavior).

Although strictly a subset of AL, this package is currently under review, and should be
considered a beta version.

## What's in the box?

Everything you find in Active-Logic, minus:
- Task, UTask
- Ordered and mutable composites
- Decorators

## Setup

- Open the Unity Package manager and use the [+] symbol to add the following URL: `https://github.com/active-logic/active-lt.git`
- You may also clone the repository and add from disk, navigating to the `package.json`.

## Resources

The [frogger tutorial](https://github.com/active-logic/active-lt-demos) teaches you how to use Active-LT step by step (in progress)

## FAQ

### Where are the docs?

Please read the documentation on the [main active logic repo](https://github.com/active-logic/activelogic-cs).
In the near future, a separate tutorial and demo will be provided, so you can (more) easily get started with Active-LT.

### Can I release a game made using Active-LT?

- Under AGPL (via Github) you can, provided you share the source (please read the license).
- Under the Unity Asset Store EULA, you will be able to release games made using the 'LT' library without publishing
game source; please take patience as requirements for Unity Asset Store releases are more stringent than releasing on Github,
so I need a few days to clear everything.

### How is this different from 'active-logic-cs' on Github?

The [active-logic-cs](https://github.com/active-logic/activelogic-cs) package is engine agnostic, and does not feature the Unity integration. If you are not using Unity, you do not need this package.

*Happy coding!*

~Tea
