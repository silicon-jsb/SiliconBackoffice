using Newtonsoft.Json;
using SiliconBackoffice.Data.Models;
using SiliconBackoffice.GraphQL;
using System.Text;

namespace SiliconBackoffice.Data.Services;

public class GraphQLService
{
    private readonly HttpClient _httpClient;

    public GraphQLService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Course>> GetCoursesAsync()
    {
        var query = new
        {
            query = CourseQuery.GetCourses
        };

        var response = await _httpClient.PostAsync("http://localhost:7022/api/graphql",
            new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json"));

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseResult<CoursesResponse>>(json);

        return result.Data.GetAllCourses;
    }

    public async Task<Course> GetCourseByIdAsync(string id)
    {
        var query = new
        {
            query = CourseQuery.GetCourseById,
            variables = new { id }
        };

        var response = await _httpClient.PostAsync("http://localhost:7022/api/graphql",
            new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json"));

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseResult<CourseResponse>>(json);

        return result.Data.GetOneCourse;
    }

    public async Task<Course> CreateCourseAsync(CourseCreateRequest course)
    {
        var mutation = new
        {
            query = CourseMutation.CreateCourse,
            variables = new { course }
        };

        var response = await _httpClient.PostAsync("http://localhost:7022/api/graphql",
            new StringContent(JsonConvert.SerializeObject(mutation), Encoding.UTF8, "application/json"));

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseResult<CourseMutationResponse>>(json);

        return result.Data.CreateCourse;
    }

    public async Task<Course> UpdateCourseAsync(string id, CourseUpdateRequest course)
    {
        var mutation = new
        {
            query = CourseMutation.UpdateCourse,
            variables = new { id, course }
        };

        var response = await _httpClient.PostAsync("http://localhost:7022/api/graphql",
            new StringContent(JsonConvert.SerializeObject(mutation), Encoding.UTF8, "application/json"));

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseResult<CourseMutationResponse>>(json);

        return result.Data.UpdatedCourse;
    }

    public async Task<bool> DeleteCourseAsync(string id)
    {
        var mutation = new
        {
            query = CourseMutation.DeleteCourse,
            variables = new { id }
        };

        var response = await _httpClient.PostAsync("http://localhost:7022/api/graphql",
            new StringContent(JsonConvert.SerializeObject(mutation), Encoding.UTF8, "application/json"));

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseResult<CourseMutationResponse>>(json);

        return result.Data.DeleteSuccess;
    }
}
