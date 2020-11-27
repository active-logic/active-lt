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

The [frogger tutorial](https://github.com/active-logic/active-lt-demos/Frogger) teaches you how to use Active-LT step by step (in progress)

## FAQ

### Where are the docs?

Read the documentation on the [main active logic repository](https://github.com/active-logic/activelogic-cs).

### Can I release a game made using Active-LT?

Under AGPL (via Github), provided you share the source (please read the [license](LICENSE)).

Under the Unity Asset Store EULA, you may release games made using this library without publishing
game source; as requirements for Unity Asset Store releases are more stringent than releasing on Github,
I need a few days to clear everything.

### How is this different from 'active-logic-cs' on Github?

The [active-logic-cs](https://github.com/active-logic/activelogic-cs) package does not feature the Unity integration; if you are using Unity, prefer the LT package (here) or the [full-featured Unity Asset](https://assetstore.unity.com/packages/tools/ai/active-logic-151850).

### How do I upgrade to the full version?

Upgrading is easy. Verified steps are described [here](https://github.com/active-logic/activelogic-cs/blob/master/Doc/Upgrading.md#1-remove-active-lt).

*Happy coding!*

~Tea
