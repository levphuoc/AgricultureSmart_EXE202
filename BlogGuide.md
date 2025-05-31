# Hướng dẫn sử dụng API Blog

Hướng dẫn này sẽ giúp bạn hiểu cách sử dụng API để tạo và quản lý blog cá nhân trong hệ thống AgricultureSmart.

## 1. Đăng nhập

Trước khi tạo blog, bạn cần đăng nhập để lấy token xác thực:

```
POST /api/Auth/login
Content-Type: application/json

{
  "username": "your_username",
  "password": "your_password"
}
```

Lưu lại token nhận được từ phản hồi để sử dụng cho các yêu cầu tiếp theo.

## 2. Tạo bài viết blog mới

```
POST /api/Blog
Content-Type: application/json
Authorization: Bearer {your_token}

{
  "categoryId": 1,
  "title": "Tiêu đề bài viết của tôi",
  "content": "Nội dung bài viết của tôi...",
  "featuredImage": "đường_dẫn_hình_ảnh.jpg",
  "slug": "tieu-de-bai-viet-cua-toi",
  "status": "draft"
}
```

Lưu ý:
- `categoryId`: ID của danh mục blog (bạn cần biết danh mục nào phù hợp với bài viết của mình)
- `slug`: URL thân thiện cho bài viết, thường là tiêu đề đã được chuyển đổi (không dấu, thay khoảng trắng bằng dấu gạch ngang)
- `status`: Trạng thái bài viết, có thể là "draft" (nháp) hoặc "published" (đã xuất bản)

## 3. Cập nhật bài viết

```
PUT /api/Blog/{id}
Content-Type: application/json
Authorization: Bearer {your_token}

{
  "id": 1,
  "categoryId": 1,
  "title": "Tiêu đề bài viết đã cập nhật",
  "content": "Nội dung bài viết đã cập nhật...",
  "featuredImage": "đường_dẫn_hình_ảnh.jpg",
  "slug": "tieu-de-bai-viet-da-cap-nhat",
  "status": "draft"
}
```

## 4. Xuất bản bài viết

```
POST /api/Blog/{id}/publish
Authorization: Bearer {your_token}
```

## 5. Hủy xuất bản bài viết

```
POST /api/Blog/{id}/unpublish
Authorization: Bearer {your_token}
```

## 6. Xóa bài viết

```
DELETE /api/Blog/{id}
Authorization: Bearer {your_token}
```

## 7. Xem danh sách bài viết

```
GET /api/Blog
```

## 8. Xem chi tiết bài viết theo ID

```
GET /api/Blog/{id}
```

## 9. Xem chi tiết bài viết theo slug

```
GET /api/Blog/slug/{slug}
```

## 10. Xem danh sách bài viết theo danh mục

```
GET /api/Blog/category/{categoryId}
```

## Lưu ý quan trọng

- Đảm bảo rằng bạn đã đăng nhập và có token hợp lệ khi thực hiện các thao tác tạo, cập nhật hoặc xóa bài viết.
- Khi tạo slug, hãy đảm bảo rằng nó là duy nhất và không chứa các ký tự đặc biệt hoặc khoảng trắng.
- Hình ảnh nổi bật (featuredImage) nên được tải lên trước và lưu trữ ở một nơi có thể truy cập được, sau đó chỉ cần cung cấp đường dẫn đến hình ảnh đó. 