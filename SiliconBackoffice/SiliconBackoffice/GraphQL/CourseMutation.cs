namespace SiliconBackoffice.GraphQL;

public class CourseMutation
{

    public const string CreateCourse = @"
    mutation CreateCourse($course: CourseInput!) {
      createCourse(course: $course) {
        id
        title
        author
        // Include any other fields you need here
      }
    }";

    public const string UpdateCourse = @"
    mutation UpdateCourse($id: ID!, $course: CourseInput!) {
      updateCourse(id: $id, course: $course) {
        id
        title
        // Include any other fields you need here
      }
    }";

    public const string DeleteCourse = @"
    mutation DeleteCourse($id: ID!) {
      deleteCourse(id: $id) {
        id
        title
        // Include any other fields you need here
      }
    }";
}
