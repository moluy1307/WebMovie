const commonJS = {
    /**
     * Format ngày tháng thành định dạng dd/mm/yyyy
     * CreatedBy: huynq 10/12/2022
     * @param {*} dateTime
     * @returns
     */
    formatDate(dateTime) {
        try {
            if (dateTime != null && dateTime != undefined) {
                //Chuyển thành dữ liệu ngày tháng
                dateTime = new Date(dateTime);

                let date = dateTime.getDate();
                date = date < 10 ? `0${date}` : date;

                let month = dateTime.getMonth() + 1;
                month = month < 10 ? `0${month}` : month;

                let year = dateTime.getFullYear();

                return `${date}/${month}/${year}`;
            } else {
                return '';
            }
        } catch (err) {
            this.$MToastMessage({
                titleToast: 'Lỗi',
                messageToast: err,
                showToastMessage: true,
            });
            return '';
        }
    },

    /**
     * Format ngày tháng thành định dạng yyyy-mm-dd
     * CreatedBy: huynq 10/12/2022
     * @param {*} dateTime
     * @returns
     */
    bindingFormatDate(dateTime) {
        try {
            if (dateTime != null && dateTime != undefined) {
                //Chuyển thành dữ liệu ngày tháng
                dateTime = new Date(dateTime);

                let date = dateTime.getDate();
                date = date < 10 ? `0${date}` : date;

                let month = dateTime.getMonth() + 1;
                month = month < 10 ? `0${month}` : month;

                let year = dateTime.getFullYear();

                return `${year}-${month}-${date}`;
            } else {
                return '';
            }
        } catch (err) {
            this.$MToastMessage({
                titleToast: 'Lỗi',
                messageToast: err,
                showToastMessage: true,
            });
            return '';
        }
    },

    /**
     * Format tiền thành định dạng 0đ
     * CreatedBy: huynq 10/12/2022
     * @param {*} number
     * @returns
     */
    formatCurrency(number) {
        try {
            if (number != null || number != undefined) {
                return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(number);
            }
            return '';
        } catch (err) {
            this.$MToastMessage({
                titleToast: 'Lỗi',
                messageToast: err,
                showToastMessage: true,
            });
            return '';
        }
    },

    /**
     * Format giới tính thành định dạng Nam/Nữ/Khác
     * CreatedBy: huynq 10/12/2022
     * @param {*} gender
     * @returns
     */
    formatGender(gender) {
        try {
            if (gender == 1) {
                return 'Nữ';
            } else if (gender == 0) {
                return 'Nam';
            } else return 'Khác';
        } catch (err) {
            this.$MToastMessage({
                titleToast: 'Lỗi',
                messageToast: err,
                showToastMessage: true,
            });
            return '';
        }
    },
};

export default commonJS;
