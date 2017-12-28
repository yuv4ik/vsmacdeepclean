using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "VSMacDeepClean",
    Namespace = "VSMacDeepClean",
    Version = "1.0"
)]

[assembly: AddinName("DeepClean")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription("Easily delete /bin, /obj and /packages directories via Build menu.")]
[assembly: AddinAuthor("Evgeny Zborovsky")]
