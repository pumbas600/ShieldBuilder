public interface IShieldBuilder
{
    void Initialise();

    void SwapColours();

    void SetTab(int tab);

    void SaveShield();
}

public enum ShieldBuilderTab { PATTERNS, COLOURS }
