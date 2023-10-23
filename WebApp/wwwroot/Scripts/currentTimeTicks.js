function updateCurrentTime() {
    var currentTimeElement = document.getElementById("currentTime");
    if (currentTimeElement) {
        var currentDate = new Date();
        var day = currentDate.getDate();
        var month = currentDate.getMonth() + 1; // Lưu ý: Tháng trong JavaScript bắt đầu từ 0
        var year = currentDate.getFullYear();
        var hour = currentDate.getHours();
        var minute = currentDate.getMinutes();
        var second = currentDate.getSeconds();

        var formattedDate = `${hour}:${minute}:${second} Thứ ${currentDate.getDay()}, ${day} tháng ${month}, ${year}`;
        currentTimeElement.textContent = formattedDate;

    }
}
// Cập nhật thời gian mỗi giây
setInterval(updateCurrentTime, 1000);
