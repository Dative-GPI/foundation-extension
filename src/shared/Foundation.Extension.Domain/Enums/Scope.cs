namespace Foundation.Extension.Domain.Enums
{
    public enum Scope
    {
        None = 0,
        Public = 0b111,
        Application = 0b100,
        Organisation = 0b10,
        User = 0b01
    }
}