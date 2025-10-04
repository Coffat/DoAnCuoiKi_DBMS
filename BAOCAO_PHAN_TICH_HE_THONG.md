# BÁO CÁO PHÂN TÍCH HỆ THỐNG QUẢN LÝ NHÂN SỰ SIÊU THỊ MINI

## 1.1 Mô tả bài toán

Hệ thống quản lý nhân sự siêu thị mini giải quyết vấn đề quản lý và điều hành hoạt động nhân sự trong môi trường bán lẻ. Hệ thống kết nối các bộ phận quản lý với nhân viên một cách hiệu quả, do đó nó sẽ có những nhóm đối tượng sau:

**Ban Giám đốc (Quản lý)**: đây là đối tượng có quyền cao nhất trong hệ thống, thực hiện việc quản lý tổng thể hoạt động của siêu thị. Họ có thể xem tất cả thông tin nhân viên, phân ca làm việc, khóa công và duyệt các đơn từ quan trọng.

**Nhân viên HR (Nhân sự)**: đây là nhóm đối tượng chuyên quản lý nhân sự, họ thực hiện công việc tuyển dụng, quản lý hồ sơ nhân viên, phân ca làm việc, duyệt đơn từ và tạo tài khoản người dùng. Họ có quyền truy cập đầy đủ vào hệ thống quản lý nhân viên và có thể xem các báo cáo chi tiết.

**Kế toán**: nhóm đối tượng này chuyên về tài chính và lương bổng. Họ không tham gia quản lý nhân viên trực tiếp nhưng có nhiệm vụ tính toán lương, quản lý bảng lương, khấu trừ và phát lương cho nhân viên. Họ sử dụng hệ thống để tính toán tự động các khoản lương dựa trên dữ liệu chấm công.

**Nhân viên**: nhóm đối tượng này là lực lượng lao động chính của siêu thị, bao gồm nhân viên bán hàng, thu ngân, kho và các bộ phận khác. Họ sử dụng hệ thống để xem lịch làm việc cá nhân, chấm công, gửi đơn từ và xem thông tin lương của mình.

Mục tiêu của hệ thống là quản lý việc này một cách tự động và hiệu quả quy trình quản lý nhân sự từ khâu tuyển dụng, phân ca làm việc, chấm công, duyệt đơn từ hay tính lương, không chỉ giảm thiểu các lỗi thủ công mà còn tiện lợi cho việc theo dõi, phân tích dữ liệu qua đó giúp cho doanh nghiệp đạt được mục đích quản lý hiệu quả và cung cấp cho nhân viên môi trường làm việc minh bạch và công bằng.

## 1.2 Mô tả dữ liệu

### Mô tả các tập thực thể

Hệ thống sẽ cung cấp dịch vụ quản lý cho nhiều loại nhân viên khác nhau. Ở đây sẽ chia thành các phòng ban chính đó là: Ban Giám đốc, Phòng Nhân sự, Phòng Kế toán, Bộ phận Bán hàng, Bộ phận Kho và Bộ phận Thu ngân. Mỗi phòng ban sẽ có các chức vụ khác nhau như Giám đốc, Trưởng phòng và các nhân viên chuyên môn.

Quản lý dịch vụ nhân sự không thể thiếu dữ liệu về nhân viên. Trong hệ thống này, mỗi nhân viên sẽ có các thông tin quan trọng như mã nhân viên, họ tên, ngày sinh, giới tính, số điện thoại, email, địa chỉ, ngày vào làm, phòng ban, chức vụ và lương cơ bản. Mỗi nhân viên chỉ được phân vào một phòng ban và có một chức vụ cụ thể trong thời điểm nhất định.

Ví dụ: anh A làm việc ở Bộ phận Bán hàng với chức vụ Nhân viên bán hàng, thì anh A sẽ có lịch làm việc và chấm công theo ca của bộ phận này.

Khi nhân viên được phân ca làm việc, hệ thống sẽ tạo lịch phân ca. Thông tin lịch phân ca bao gồm: Mã lịch, mã nhân viên, ngày làm việc, mã ca làm việc, trạng thái (dự kiến/khóa/hủy) và ghi chú. Lịch này sẽ được sử dụng để theo dõi việc phân công công việc cho từng nhân viên.

Trong hệ thống quản lý nhân sự, các hoạt động như chấm công, nghỉ phép hoặc làm thêm giờ đều được quản lý thông qua các đơn từ và bản ghi chấm công. Mỗi bản ghi chấm công sẽ có thông tin về giờ vào, giờ ra, giờ công thực tế, số phút đi trễ và về sớm. Đơn từ sẽ được phân loại thành đơn xin nghỉ phép (NGHI) hoặc đơn xin làm thêm giờ (OT) với trạng thái chờ duyệt, đã duyệt hoặc từ chối.

Mỗi tháng, hệ thống sẽ cần phải tính toán lương cho nhân viên. Việc này sẽ dễ dàng cho việc tạo số liệu tổng kết chi phí nhân sự. Bảng lương bao gồm: Mã bảng lương, năm, tháng, mã nhân viên, lương cơ bản, tổng giờ công, giờ OT, phụ cấp, khấu trừ, thuế bảo hiểm và thực lãnh. Đồng thời sẽ kèm thêm trạng thái để xác định bảng lương đã được khóa hay chưa.

Sau khi lịch phân ca được tạo, việc chấm công cho nhân viên sẽ được tiến hành. Thông tin chấm công bao gồm: Mã chấm công, mã nhân viên, ngày làm việc, giờ vào, giờ ra, giờ công thực tế, số phút đi trễ và về sớm. Mỗi lần chấm công sẽ có một bản ghi duy nhất cho mỗi nhân viên trong một ngày.

Khi có nhu cầu nghỉ phép hoặc làm thêm giờ, nhân viên sẽ tạo đơn từ. Họ sẽ ghi lại thông tin về loại đơn, thời gian từ lúc nào đến lúc nào, số giờ, lý do và trạng thái đơn từ. Tương tự như chấm công, quá trình duyệt đơn từ cũng có quy trình rõ ràng với người duyệt được ghi nhận trong hệ thống.

Trong quá trình làm việc, có thể xảy ra các tình huống cần điều chỉnh như thay đổi ca làm việc, điều chuyển nhân viên giữa các phòng ban. Mỗi thay đổi này sẽ được lưu trữ lại để dễ dàng theo dõi lịch sử công việc và thay đổi của nhân viên.

Hệ thống quản lý nhiều ca làm việc khác nhau, từ ca sáng, ca chiều đến ca đêm. Mỗi ca làm việc sẽ có một mã ca và những thông tin như tên ca, giờ bắt đầu, giờ kết thúc, hệ số ca và mô tả. Điều này giúp hệ thống dễ dàng phân loại và quản lý thời gian làm việc của nhân viên.

Mỗi ca làm việc sẽ có một mức hệ số riêng biệt. Hệ thống sẽ lưu trữ thông tin về hệ số của từng ca, ví dụ ca đêm có hệ số 1.5, ca bình thường có hệ số 1.0. Điều này cho phép tính toán lương chính xác dựa trên loại ca làm việc.

### Về phần lưu trữ các thông tin của người dùng:

Tất cả người dùng trong hệ thống sẽ cần phải có tài khoản để đăng nhập và sử dụng dịch vụ. Tài khoản sẽ chứa các thông tin đăng nhập cơ bản như tên đăng nhập, mật khẩu hash và vai trò của người dùng (HR, QuanLy, KeToan, NhanVien). Vai trò này sẽ giúp hệ thống xác định quyền hạn của mỗi người dùng khi truy cập vào hệ thống.

Ban Giám đốc là người có quyền quản lý toàn bộ hệ thống và có quyền xem tất cả thông tin liên quan đến hoạt động của siêu thị. Thông tin của Ban Giám đốc sẽ được lưu trữ trong hệ thống bao gồm họ tên, thông tin liên lạc và quyền truy cập cao nhất. Mỗi thành viên Ban Giám đốc cũng sẽ có một tài khoản để đăng nhập vào hệ thống và quản lý hoạt động.

Nhân viên HR là những người đảm nhiệm các công việc như quản lý hồ sơ nhân viên, phân ca làm việc, duyệt đơn từ hoặc quản lý các tác vụ khác liên quan đến nhân sự. Họ cũng có tài khoản đăng nhập vào hệ thống để thực hiện công việc. Thông tin của nhân viên HR được lưu trữ bao gồm họ tên, thông tin liên lạc và quyền truy cập cấp cao.

Kế toán là người chuyên về tính toán lương và quản lý tài chính liên quan đến nhân sự. Mỗi kế toán cũng có tài khoản để đăng nhập và thực hiện các thao tác như tính lương, khóa/mở bảng lương, v.v. Hệ thống sẽ lưu trữ thông tin của kế toán bao gồm họ tên, thông tin liên lạc và các quyền truy cập chuyên biệt về tài chính.

Nhân viên là người sử dụng hệ thống để xem thông tin cá nhân và thực hiện các hoạt động liên quan đến công việc. Mỗi nhân viên cũng có tài khoản để đăng nhập và thực hiện các thao tác như xem lịch làm việc, chấm công, gửi đơn từ, v.v. Hệ thống sẽ lưu trữ thông tin của nhân viên bao gồm họ tên, thông tin cá nhân, thông tin công việc và quyền truy cập phù hợp với vai trò.

### Mô tả các mối quan hệ

Mỗi người dùng có một tài khoản. Mối quan hệ này là 1-1 có nghĩa là một người dùng chỉ có thể có một tài khoản và ngược lại.

Người dùng chia làm 4 loại: HR, Quản lý (QuanLy), Kế toán (KeToan) và Nhân viên (NhanVien).

Một nhân viên thuộc về một phòng ban, nhưng một phòng ban có thể có nhiều nhân viên.

Một nhân viên có một chức vụ, nhưng một chức vụ có thể được gán cho nhiều nhân viên.

Một nhân viên có thể có nhiều lịch phân ca (theo các ngày khác nhau), nhưng mỗi lịch phân ca chỉ thuộc về một nhân viên.

Một ca làm việc có thể được sử dụng trong nhiều lịch phân ca, nhưng mỗi lịch phân ca chỉ tương ứng với một ca làm việc.

Mỗi nhân viên có thể có nhiều bản ghi chấm công (theo các ngày khác nhau), trong đó mỗi bản ghi chấm công chỉ thuộc về một nhân viên trong một ngày cụ thể.

Một nhân viên có thể gửi nhiều đơn từ, mỗi đơn từ chỉ thuộc về một nhân viên.

Một người dùng (có quyền duyệt) có thể duyệt nhiều đơn từ, nhưng mỗi đơn từ chỉ được duyệt bởi một người.

Mỗi nhân viên có thể có nhiều bảng lương (theo các tháng khác nhau), mỗi bảng lương chỉ thuộc về một nhân viên trong một kỳ lương cụ thể.

## 1.3 Mô tả chức năng

Có bốn nhóm người dùng chính là: Ban Giám đốc (QuanLy), HR, Kế toán (KeToan) và Nhân viên (NhanVien).

### Chức năng chung:

+ **Đăng nhập**: Mỗi người dùng có tài khoản riêng để đăng nhập và truy cập các chức năng tương ứng với quyền hạn của mình.
+ **Quản lý thông tin cá nhân**: Người dùng có thể xem và cập nhật một số thông tin cá nhân được phép chỉnh sửa.

### Chức năng của Ban Giám đốc (QuanLy):

+ **Quản lý nhân viên**: Xem thông tin tất cả nhân viên trong hệ thống, theo dõi tình trạng làm việc.
+ **Quản lý lịch phân ca**: Xem và điều chỉnh lịch phân ca của nhân viên, phân công công việc hợp lý.
+ **Chấm công và khóa công**: Theo dõi tình hình chấm công và thực hiện khóa công theo tháng.
+ **Duyệt đơn từ**: Duyệt các đơn từ nghỉ phép hoặc làm thêm giờ của nhân viên.
+ **Thống kê và đánh giá**: Xem các báo cáo tổng quan về hoạt động nhân sự của siêu thị.

### Chức năng của HR:

+ **Quản lý nhân viên**: Thêm, sửa, xóa thông tin nhân viên; quản lý hồ sơ nhân sự đầy đủ.
+ **Quản lý người dùng**: Tạo, cập nhật và khóa/mở khóa tài khoản đăng nhập cho nhân viên.
+ **Quản lý phòng ban và chức vụ**: Tạo và quản lý cơ cấu tổ chức của siêu thị.
+ **Quản lý ca làm việc**: Tạo và quản lý các ca làm việc với giờ giấc và hệ số phù hợp.
+ **Phân ca cho nhân viên**: Lập lịch làm việc cho từng nhân viên theo ngày, tuần, tháng.
+ **Duyệt đơn từ**: Xử lý và duyệt các đơn từ của nhân viên.
+ **Báo cáo nhân sự**: Tạo và xem các báo cáo chi tiết về hoạt động nhân sự.

### Chức năng của Kế toán:

+ **Tính lương**: Thực hiện tính toán lương tự động dựa trên dữ liệu chấm công và ca làm việc.
+ **Quản lý bảng lương**: Xem, chỉnh sửa và khóa/mở bảng lương theo kỳ.
+ **Quản lý phụ cấp và khấu trừ**: Cập nhật các khoản phụ cấp, khấu trừ, thuế và bảo hiểm.
+ **Báo cáo tài chính**: Tạo báo cáo về chi phí nhân sự, thống kê lương theo phòng ban.
+ **Xuất phiếu lương**: In và xuất phiếu lương cho nhân viên.

### Chức năng của Nhân viên:

+ **Xem lịch làm việc**: Xem lịch phân ca cá nhân theo ngày, tuần để biết khi nào cần đi làm.
+ **Chấm công**: Xem thông tin chấm công cá nhân, giờ vào/ra và các thống kê liên quan.
+ **Tạo đơn từ**: Gửi đơn xin nghỉ phép hoặc đơn xin làm thêm giờ khi có nhu cầu.
+ **Quản lý đơn từ**: Theo dõi trạng thái các đơn từ đã gửi (chờ duyệt, đã duyệt, từ chối).
+ **Xem phiếu lương**: Xem thông tin lương cá nhân theo từng tháng, bao gồm chi tiết các khoản thu/chi.

## 1.4 Về phần giao diện:

### Phần chung: Là những form mà tất cả người dùng đều thao tác

**Giao diện đăng nhập (frmLogin)** cho phép người dùng truy cập vào hệ thống với các thành phần như: Label tên người dùng, Label mật khẩu, TextBox nhập tên người dùng, TextBox nhập mật khẩu (mật khẩu được ẩn), Button đăng nhập, Checkbox ghi nhớ đăng nhập và Link quên mật khẩu. Các control được bố trí đẹp mắt với màu sắc hiện đại, button đăng nhập nổi bật ở phía dưới và có xử lý phím tắt Enter.

### Phần giao diện riêng: Bao gồm các phần giao diện con được quản lý trong một giao diện chính để tối ưu trải nghiệm

**Giao diện chính (frmMain)** là form container chính với sidebar navigation được phân quyền theo vai trò người dùng. Giao diện sử dụng thiết kế hiện đại với màu chủ đạo tím (#7C4DFF), có panel nội dung chính để hiển thị các form con và statusbar hiển thị thông tin trạng thái.

**Giao diện HR (vai trò HR)** cho phép quản lý đầy đủ các chức năng nhân sự:
+ **Menu Quản lý nhân sự**: Hiển thị submenu với các tùy chọn "Hồ sơ Nhân viên", "Phòng ban & Chức vụ", "Lịch phân ca" và "Duyệt Đơn từ".
+ **Quản lý người dùng**: Button riêng để truy cập form quản lý tài khoản người dùng (frmQuanLyNguoiDung).
+ **Quản lý ca làm việc**: Button để mở form quản lý ca làm việc (frmCaLam).
+ **Chấm công**: Button để truy cập form chấm công (frmChamCong).

**Giao diện Quản lý (vai trò QuanLy)** tập trung vào giám sát và duyệt:
+ **Menu Quản lý nhân sự**: Có thể xem thông tin nhân viên và lịch phân ca.
+ **Quản lý ca làm việc**: Quyền xem và điều chỉnh ca làm việc.
+ **Chấm công**: Có thể xem và khóa công theo tháng.
+ **Menu Nghiệp vụ**: Hiển thị tùy chọn "Duyệt Đơn từ" để xử lý các đơn từ của nhân viên.

**Giao diện Kế toán (vai trò KeToan)** chuyên về tài chính:
+ **Menu Quản lý nhân sự**: Chỉ có quyền xem thông tin nhân viên để phục vụ tính lương.
+ **Menu Tiền lương**: Hiển thị tùy chọn "Quản lý Bảng lương" để truy cập form tính lương (frmBangLuong).
+ **Chấm công**: Có thể xem dữ liệu chấm công để tính toán lương.

**Giao diện Nhân viên (vai trò NhanVien)** đơn giản và tập trung vào nhu cầu cá nhân:
+ **Chấm công**: Button để xem thông tin chấm công cá nhân.
+ **Menu Nghiệp vụ**: Hiển thị submenu với "Tạo đơn từ" và "Xem đơn của tôi".
+ **Thông tin cá nhân**: Button để xem và cập nhật thông tin cá nhân.

**Các form chức năng chính**:

+ **frmNhanVien**: Form quản lý hồ sơ nhân viên với DataGridView hiển thị danh sách, panel thông tin chi tiết và các button CRUD. Form được phân quyền theo vai trò người dùng.

+ **frmCaLam**: Form quản lý ca làm việc với khả năng thêm/sửa/xóa ca, thiết lập giờ làm và hệ số ca.

+ **frmPhanCa**: Form xem lịch phân ca theo tuần với khả năng filter theo nhân viên và tìm kiếm.

+ **frmLichTuan**: Form tạo và quản lý lịch tuần với tính năng sao chép lịch, khóa/mở khóa tuần.

+ **frmChamCong**: Form chấm công với chức năng check-in/check-out và xem báo cáo chấm công.

+ **frmBangLuong**: Form tính và quản lý lương với khả năng tính toán tự động và xuất phiếu lương.

+ **frmDuyetDonTu**: Form duyệt đơn từ cho HR và Quản lý với danh sách đơn từ chờ duyệt.

+ **frmTaoDonTu** và **frmXemDonCuaToi**: Form cho nhân viên tạo và theo dõi đơn từ cá nhân.

## 1.5 Kiến trúc Database và Business Logic

### Cấu trúc Database chi tiết

**Bảng chính và ràng buộc:**

+ **NguoiDung**: Quản lý tài khoản đăng nhập với các ràng buộc về vai trò (HR, QuanLy, KeToan, NhanVien), tên đăng nhập duy nhất và trạng thái kích hoạt.

+ **NhanVien**: Lưu trữ thông tin nhân viên với foreign key tới NguoiDung, PhongBan, ChucVu. Có ràng buộc về trạng thái (DangLam, Nghi, TamNghi), email và điện thoại duy nhất khi không NULL.

+ **PhongBan và ChucVu**: Chuẩn hóa cấu trúc tổ chức với tên duy nhất và trạng thái kích hoạt.

+ **CaLam**: Định nghĩa ca làm việc với validation về thời gian và hệ số ca, kiểm tra overlap giữa các ca làm việc.

+ **LichPhanCa**: Lịch phân ca với ràng buộc không được trùng thời gian, tổng thời gian không vượt 16 giờ/ngày.

+ **ChamCong**: Bản ghi chấm công với constraint giờ vào < giờ ra, mỗi nhân viên chỉ có 1 record/ngày.

+ **DonTu**: Đơn từ với validation loại đơn (NGHI/OT), thời gian hợp lệ và quy trình duyệt.

+ **BangLuong**: Bảng lương với constraint duy nhất theo (Nam, Thang, MaNV) và trạng thái khóa.

### Views và Functions đã triển khai

**6 Views chính:**
+ `vw_CongThang`: Tổng hợp công theo tháng với tính toán đi trễ/về sớm
+ `vw_BangLuong_ChiTiet`: Hiển thị bảng lương kèm thông tin nhân viên, phòng ban
+ `vw_NhanVien_Full`: Thông tin nhân viên đầy đủ với phòng ban, chức vụ
+ `vw_DonTu_ChiTiet`: Đơn từ chi tiết với tên nhân viên và người duyệt
+ `vw_BaoCaoNhanSu`: Thống kê nhân sự theo phòng ban và chức vụ
+ `vw_Lich_ChamCong_Ngay`: Kết hợp lịch phân ca và chấm công theo ngày

**10 Functions (5 Scalar + 5 Table-Valued) - 100% được sử dụng:**

*Scalar Functions:*
+ `fn_SoPhutDuong`: Tính số phút dương (âm => 0) để tính đi trễ/về sớm
+ `fn_TongLuongThang`: Tính tổng lương thực nhận theo tháng
+ `fn_SoNgayLamViec`: Tính số ngày làm việc thực tế trong tháng
+ `fn_TyLeDiTre`: Tính tỷ lệ đi trễ của nhân viên (%)
+ `fn_TinhTuoi`: Tính tuổi chính xác từ ngày sinh

*Table-Valued Functions:*
+ `fn_KhungCa`: Trả về thông tin ca làm việc của nhân viên trong ngày
+ `tvf_LichTheoTuan`: Hiển thị lịch theo tuần (7 dòng Mon-Sun)
+ `tvf_NhanVienTheoPhongBan`: Danh sách nhân viên theo phòng ban với thông tin chi tiết
+ `tvf_BaoCaoChamCongThang`: Báo cáo chấm công chi tiết theo tháng
+ `tvf_LichSuDonTuNhanVien`: Lịch sử đơn từ của nhân viên trong khoảng thời gian

### Stored Procedures nâng cao (38 procedures)

**Quản lý CaLam (5 procedures):**
+ `sp_CaLam_GetAll`: Lấy tất cả ca làm việc đang kích hoạt
+ `sp_CaLam_GetById`: Lấy thông tin chi tiết ca làm việc theo ID
+ `sp_CaLam_Insert`: Thêm ca làm việc mới với validation
+ `sp_CaLam_Update`: Cập nhật thông tin ca làm việc
+ `sp_CaLam_Delete`: Xóa ca làm việc (chỉ HR)

**Quản lý PhongBan (4 procedures):**
+ `sp_PhongBan_Insert`: Thêm phòng ban mới
+ `sp_PhongBan_Update`: Cập nhật thông tin phòng ban
+ `sp_PhongBan_Delete`: Xóa phòng ban (soft delete)
+ `sp_PhongBan_GetAll`: Lấy danh sách phòng ban đang kích hoạt

**Quản lý ChucVu (4 procedures):**
+ `sp_ChucVu_Insert`: Thêm chức vụ mới
+ `sp_ChucVu_Update`: Cập nhật thông tin chức vụ
+ `sp_ChucVu_Delete`: Xóa chức vụ (soft delete)
+ `sp_ChucVu_GetAll`: Lấy danh sách chức vụ đang kích hoạt

**Quản lý NhanVien (8 procedures):**
+ `sp_ThemMoiNhanVien`: Tạo nhân viên mới với tùy chọn tạo tài khoản
+ `sp_GetPhongBanChucVu`: Lấy danh sách phòng ban và chức vụ cho ComboBox
+ `sp_GetNhanVienFull`: Lấy thông tin nhân viên đầy đủ từ view
+ `sp_UpdateNhanVienWithPhongBanChucVu`: Cập nhật phòng ban và chức vụ
+ `sp_NhanVien_Delete`: Xóa nhân viên (với ràng buộc dữ liệu liên quan)
+ `sp_NhanVien_UpdateTrangThai`: Cập nhật trạng thái nhân viên
+ `sp_NhanVien_GetThongTinCaNhan`: Lấy thông tin cá nhân cho form profile
+ `sp_NhanVien_UpdateThongTinCaNhan`: Cập nhật thông tin cá nhân

**Quản lý LichPhanCa (4 procedures):**
+ `sp_LichPhanCa_Insert`: Thêm lịch phân ca với validation overlap
+ `sp_LichPhanCa_Update`: Cập nhật lịch phân ca
+ `sp_LichPhanCa_Delete`: Xóa lịch phân ca
+ `sp_LichPhanCa_GetByNhanVien`: Lấy lịch theo nhân viên và khoảng thời gian

**Advanced Procedures (13 procedures):**
+ `sp_DuyetDonTu`: Duyệt đơn từ với đồng bộ LichPhanCa và ChamCong
+ `sp_KhoaCongThang`: Khóa công theo tháng với validation
+ `sp_MoKhoaCongThang`: Mở khóa công theo tháng (chỉ HR)
+ `sp_ChayBangLuong`: Tính lương tự động với isolation level SERIALIZABLE
+ `sp_DongBangLuong`: Khóa bảng lương với validation đầy đủ
+ `sp_CheckIn`: Chấm công vào với tính toán tự động
+ `sp_CheckOut`: Chấm công ra với tính toán tự động
+ `sp_GetTrangThaiChamCong`: Lấy trạng thái chấm công hiện tại
+ `sp_LichPhanCa_CloneWeek`: Sao chép lịch tuần với tùy chọn ghi đè
+ `sp_LichPhanCa_KhoaTuan`: Khóa lịch theo tuần
+ `sp_LichPhanCa_MoKhoaTuan`: Mở khóa lịch theo tuần
+ `sp_DonTu_Insert`: Tạo đơn từ mới với validation
+ `sp_NguoiDung_DoiMatKhau`: Đổi mật khẩu với validation mật khẩu cũ

### Hệ thống bảo mật và phân quyền

**Database Roles:**
+ `r_hr`: Toàn quyền quản lý nhân viên, phân ca, duyệt đơn từ
+ `r_quanly`: Quyền giám sát, duyệt đơn từ, khóa công
+ `r_ketoan`: Quyền tính lương, quản lý bảng lương, báo cáo tài chính
+ `r_nhanvien`: Quyền xem thông tin cá nhân, tạo đơn từ

**Nguyên tắc bảo mật:**
+ Tất cả thao tác INSERT/UPDATE/DELETE phải qua Stored Procedures
+ Chỉ cấp quyền SELECT trên views/tables cho mục đích xem
+ Chỉ cấp quyền EXECUTE trên Stored Procedures cho thay đổi dữ liệu
+ Session context để bypass triggers khi cần thiết

**Triggers và Business Rules:**
+ `tr_LichPhanCa_BlockChangeWhenLocked`: Chặn sửa lịch đã khóa
+ `tr_ChamCong_BlockWhenLocked`: Chặn sửa chấm công đã khóa
+ `tr_NhanVien_ToggleAccount`: Tự động khóa/mở tài khoản khi thay đổi trạng thái nhân viên
+ `tr_ChamCong_AutoCalculate`: Tự động tính GioCong, DiTrePhut, VeSomPhut

### Workflow nghiệp vụ chi tiết

**1. Quy trình tuyển dụng và tạo tài khoản:**
```
Tạo nhân viên → Phân phòng ban/chức vụ → Tạo tài khoản đăng nhập → Gán quyền role
```

**2. Quy trình phân ca làm việc:**
```
Tạo ca làm việc → Phân ca cho nhân viên → Validation overlap/16h → Lưu lịch phân ca
```

**3. Quy trình chấm công:**
```
Check-in → Ghi giờ vào → Check-out → Ghi giờ ra → Tự động tính công/trễ/sớm
```

**4. Quy trình duyệt đơn từ:**
```
Nhân viên tạo đơn → HR/Quản lý duyệt → Cập nhật lịch/chấm công → Thông báo kết quả
```

**5. Quy trình tính lương:**
```
Khóa công tháng → Chạy tính lương → Validation → Khóa bảng lương → Xuất phiếu lương
```

## 1.6 Tính năng kỹ thuật nâng cao

### Transaction và ACID Properties

+ **Isolation Levels**: Sử dụng SERIALIZABLE cho sp_ChayBangLuong để đảm bảo consistency
+ **Locking Strategy**: UPDLOCK, ROWLOCK cho concurrent access
+ **Error Handling**: SET XACT_ABORT ON với comprehensive error messages
+ **Rollback Logic**: Automatic rollback on errors với proper cleanup

### Performance Optimization

+ **Indexing Strategy**: Filtered unique indexes cho columns có NULL values
+ **Query Optimization**: CTE với MAXRECURSION cho date ranges
+ **Connection Pooling**: ADO.NET connection management trong C#
+ **Lazy Loading**: Dynamic form loading trong WinForms

### Audit và Logging

+ **Change Tracking**: Triggers ghi lại modifications
+ **User Context**: SESSION_CONTEXT cho việc bypass triggers
+ **Error Logging**: Comprehensive error messages với error codes
+ **Data Validation**: Multiple layers validation (Database + Application)

## 1.7 Kiến trúc ứng dụng C# WinForms

### Cấu trúc Project và Pattern

**Cấu trúc thư mục:**
```
VuToanThang_23110329/
├── Forms/                    # Giao diện người dùng
│   ├── frmLogin.cs          # Form đăng nhập
│   ├── frmMain.cs           # Form chính container
│   ├── frmNhanVien.cs       # Quản lý nhân viên
│   ├── frmCaLam.cs          # Quản lý ca làm việc
│   ├── frmPhanCa.cs         # Phân ca nhân viên
│   ├── frmChamCong.cs       # Chấm công
│   ├── frmBangLuong.cs      # Quản lý lương
│   ├── frmDuyetDonTu.cs     # Duyệt đơn từ
│   ├── frmTaoDonTu.cs       # Tạo đơn từ
│   ├── frmXemDonCuaToi.cs   # Xem đơn từ cá nhân
│   ├── frmThongTinCaNhan.cs # Thông tin cá nhân
│   ├── frmQuanLyNguoiDung.cs# Quản lý tài khoản
│   └── frmPhongBan_ChucVu.cs# Quản lý cơ cấu tổ chức
├── GlobalState.cs           # Quản lý trạng thái toàn cục
├── UserSession.cs           # Quản lý phiên đăng nhập
├── Program.cs              # Entry point
└── App.config              # Cấu hình kết nối
```

**Design Patterns đã áp dụng:**

+ **Singleton Pattern**: GlobalState và UserSession để quản lý state toàn cục
+ **Factory Pattern**: Dynamic form creation trong frmMain
+ **Observer Pattern**: Event handling cho form navigation
+ **Strategy Pattern**: Role-based menu generation theo vai trò người dùng

### Authentication và Session Management

**Quá trình đăng nhập:**
1. Nhập username/password trong frmLogin
2. Validation với database qua stored procedure
3. Kiểm tra password hash (SHA256 hoặc plain text)
4. Xác thực trạng thái tài khoản (KichHoat = 1)
5. Lưu thông tin vào UserSession và GlobalState
6. Chuyển đến frmMain với quyền tương ứng

**Session Context:**
```csharp
public static class UserSession
{
    public static int MaNguoiDung { get; set; }
    public static int MaNV { get; set; }
    public static string TenDangNhap { get; set; }
    public static string HoTen { get; set; }
    public static string VaiTro { get; set; }
    public static bool IsLoggedIn { get; set; }
}
```

### Role-Based UI Generation

**Dynamic Menu theo vai trò:**

*HR (Toàn quyền):*
- Quản lý nhân viên (Hồ sơ, Phòng ban & Chức vụ, Lịch phân ca, Duyệt đơn từ)
- Quản lý người dùng (Tạo/khóa tài khoản)
- Quản lý ca làm việc
- Chấm công và báo cáo

*Quản lý (Giám sát):*
- Xem thông tin nhân viên
- Quản lý lịch phân ca và ca làm việc
- Chấm công và khóa công
- Duyệt đơn từ

*Kế toán (Tài chính):*
- Xem thông tin nhân viên (cho tính lương)
- Quản lý bảng lương và tính lương
- Chấm công (xem dữ liệu)
- Báo cáo tài chính

*Nhân viên (Cá nhân):*
- Chấm công cá nhân
- Tạo và xem đơn từ
- Thông tin cá nhân

### Data Access Layer

**Connection Management:**
```csharp
public static class GlobalState
{
    public static string ConnectionString { get; set; }
    public static string UserRole { get; set; }
    public static string Username { get; set; }
}
```

**ADO.NET Implementation:**
- Sử dụng SqlConnection với connection string từ App.config
- Stored Procedure calls với SqlCommand
- Parameter validation và SQL injection prevention
- Proper disposal và connection management

### UI Framework và Components

**Guna.UI2.WinForms Components:**
- `Guna2Button`: Modern button design với animation
- `Guna2DataGridView`: Enhanced DataGridView với styling
- `Guna2TextBox`: Styled input controls
- `Guna2Panel`: Container panels với shadow effects

**Layout Management:**
- Container form (frmMain) với sidebar navigation
- Dynamic panel content loading
- Form inheritance và reusable components
- **Advanced Responsive Design:** 3-breakpoint system với intelligent layout adaptation
  * Small screens (< 600-700px): Vertical stacking layout
  * Medium screens (600-1000px): Compact horizontal layout
  * Large screens (> 1000px): Full layout với optimal spacing
- **Smart Control Sizing:** Dynamic button và panel sizing based on available space
- **Adaptive Content Distribution:** DataGridView + Info Panel responsive positioning

## 1.8 Deployment và Testing

### Yêu cầu hệ thống

**Software Requirements:**
- Windows 10/11 (64-bit)
- .NET Framework 4.7.2+
- SQL Server 2019+ hoặc SQL Server Express
- Visual Studio 2019+ (để development)

**Hardware Requirements:**
- RAM: 4GB minimum, 8GB recommended
- Storage: 500MB cho application, 1GB cho database
- CPU: Dual-core 2.0GHz minimum

### Database Setup

**Deployment Scripts (thứ tự chạy):**
1. `01_TaoDatabase.sql` - Tạo database và các bảng
2. `02_ChucNang.sql` - Tạo views và functions
3. `03_StoredProcedures.sql` - Stored procedures cơ bản
4. `04_StoredProcedures_Advanced.sql` - Stored procedures nâng cao
5. `05_Security_Triggers.sql` - Triggers và bảo mật
6. `data_mau.sql` - Dữ liệu mẫu (optional)

**Test Data:**
- 9 tài khoản test với 4 vai trò khác nhau
- 6 phòng ban và 7 chức vụ
- 4 ca làm việc (Sáng, Chiều, Đêm, Hành chính)
- Dữ liệu chấm công và lương từ 7/2025 đến hiện tại

### Testing Strategy

**Unit Testing:**
- Database stored procedures testing
- Function validation với edge cases
- Role-based access control testing

**Integration Testing:**
- Form to database integration
- User workflow testing
- Cross-form navigation testing

**User Acceptance Testing:**
- Role-based functionality testing
- Business process validation
- UI/UX feedback collection

## 1.9 Tính năng nâng cao đã triển khai

### Database Objects Utilization - 100%

**Hệ thống đã đạt 100% sử dụng tất cả 45 database objects:**
- **38 Stored Procedures:** Tất cả được sử dụng trong các forms
- **6 Views:** 100% integration với UI forms
- **5 Scalar Functions:** Được sử dụng trong calculations và reports
- **5 Table-Valued Functions:** Được sử dụng cho complex queries và filtering

### Advanced Features

**Responsive Design thật sự:**
- 3-breakpoint responsive system cho tất cả forms
- Intelligent layout adaptation dựa trên screen size
- Dynamic control sizing và spacing calculation
- Adaptive content distribution (DataGridView + Info Panel)

**Advanced Search & Filtering:**
- Employee search by ID hoặc name trong LichPhanCaForm
- Time-range filtering trong các báo cáo
- Smart filtering với TVF integration
- Real-time search với keyboard support

**Workflow Management:**
- Complete approval workflow với sp_DuyetDonTu
- Lock/unlock mechanisms cho attendance và payroll
- Week schedule cloning với sp_LichPhanCa_CloneWeek
- Bulk operations cho payroll adjustments

**Error Handling & Validation:**
- Comprehensive DBNull handling trong tất cả repositories
- SafeConvert utility class cho type conversions
- Role-based permission validation
- Business rule validation trong stored procedures

## 1.10 So sánh với các hệ thống khác

### Ưu điểm của hệ thống

**Technical Advantages:**
+ Database design chuẩn với full normalization
+ Comprehensive business logic với 35+ stored procedures
+ Role-based security với 4-tier permission model
+ Modern UI với Guna.UI2 components
+ Transaction safety với ACID properties
+ Comprehensive audit trail và error handling

**Business Advantages:**
+ Quy trình nghiệp vụ hoàn chỉnh từ A-Z
+ Tự động hóa tính toán lương và chấm công
+ Flexible scheduling với overlap detection
+ Multi-level approval workflow
+ Comprehensive reporting và analytics

**Scalability:**
+ Modular architecture dễ mở rộng
+ Role-based design có thể thêm roles mới
+ Database schema có thể extend thêm modules
+ WinForms có thể migrate sang Web/Mobile

### Hạn chế và cải tiến

**Current Limitations:**
- Desktop-only application (WinForms)
- Single database instance (no sharding)
- Limited real-time notifications
- Basic reporting (no advanced analytics)

**Future Enhancements:**
- Web-based interface với ASP.NET Core
- Mobile app cho checkin/checkout
- Advanced reporting với Power BI
- Real-time notifications với SignalR
- Cloud deployment với Azure SQL
- API layer cho third-party integration

Việc xây dựng một hệ thống quản lý nhân sự siêu thị mini là một bước tiến quan trọng trong việc tự động hóa và tối ưu hóa quy trình quản lý nhân sự. Hệ thống không chỉ giúp tiết kiệm thời gian và nguồn lực mà còn nâng cao hiệu quả quản lý, mang lại sự minh bạch và công bằng cho nhân viên. Bằng việc tích hợp các chức năng quản lý thông tin, phân ca làm việc, chấm công và tính lương cùng với hệ thống bảo mật đa lớp, business logic phức tạp và kiến trúc ứng dụng hiện đại, doanh nghiệp có thể dễ dàng điều hành và phát triển hơn trong môi trường cạnh tranh khốc liệt của ngành bán lẻ hiện nay.
