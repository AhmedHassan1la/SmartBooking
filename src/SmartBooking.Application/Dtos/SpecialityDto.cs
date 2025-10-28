namespace SmartBooking.Application.Dtos
{
    public class SpecialityReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SpecialityCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SpecialityUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
