using Newtonsoft.Json;
using SiliconBackoffice.Data.Models;

namespace SiliconBackoffice.Data;

public class ResponseResult<T>
{
    [JsonProperty("data")]
    public T Data { get; set; }

}

    public class CoursesResponse
    {
        [JsonProperty("getCourses")]
        public List<Course> GetAllCourses { get; set; }
    }

    public class CourseResponse
    {
        [JsonProperty("getCourseById")]
        public Course GetOneCourse { get; set; }
    }

    public class CourseMutationResponse
    {
        [JsonProperty("createCourse")]
        public Course CreateCourse { get; set; }

        [JsonProperty("updateCourse")]
        public Course UpdatedCourse { get; set; }

        [JsonProperty("deleteCourse")]
        public bool DeleteSuccess { get; set; }
    }


