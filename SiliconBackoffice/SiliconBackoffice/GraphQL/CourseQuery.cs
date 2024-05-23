namespace SiliconBackoffice.GraphQL;

public class CourseQuery
{
    public const string GetCourses = @"
    query GetCourses {
      getCourses {
        id
        imageUri
        imageHeaderUri
        isBestseller
        isDigital
        categories
        title
        ingress
        starRating
        reviews
        likes
        likesInPercent
        hours
        authors {
          name
          authorImage
        }
        prices {
          currency
          price
          discount
        }
        content {
          description
          includes
          programDetails {
            id
            title
            description
          }
        }
      }
    }";

    public const string GetCourseById = @"
    query GetCourseById($id: ID!) {
      getCourseById(id: $id) {
        id
        imageUri
        imageHeaderUri
        isBestseller
        isDigital
        categories
        title
        ingress
        starRating
        reviews
        likes
        likesInPercent
        hours
        authors {
          name
          authorImage
        }
        prices {
          currency
          price
          discount
        }
        content {
          description
          includes
          programDetails {
            id
            title
            description
          }
        }
      }
    }";
}
