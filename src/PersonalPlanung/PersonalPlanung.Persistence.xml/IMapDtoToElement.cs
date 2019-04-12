namespace PersonalPlanung.Persistence.xml
{
    public interface IMapDtoToElement<D, out T>
    {
        T MapElement(D instance);
    }
}