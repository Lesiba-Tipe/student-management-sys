namespace student_management_sys.Dto
{
    public class RoleDto
    {
        public RoleDto(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public List<string> Roles { get; set; }
    }
}
