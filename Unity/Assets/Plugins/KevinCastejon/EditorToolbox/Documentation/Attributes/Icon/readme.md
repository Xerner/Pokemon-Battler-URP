# Icon
Custom *Inspector* property icon.

## How to use
Put the attribute in front of a property.
You must specify the path of the icon relative to your project folder.

## Examples
```cs
[Icon("Assets/PathToYourIcon/health.png")]
public int healthPoints;
[Icon("Assets/PathToYourIcon/sword.png")]
public int damages;
```
![](img/IconAttributeInspectorPreview.png)
