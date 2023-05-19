<template>
    <div class="modal-instruction">
        <div class="instructure-recharge">
            <h2>Hướng dẫn chuyển khoản chuyển khoản</h2>
            <div class="denomination">
                <template v-for="(den, indexDen) in denominationList" :key="indexDen">
                    <div class="custome-col col-md-2 col-4">
                        <div
                            light=""
                            height="90"
                            @click="(event) => btnSelectDenomination(event, den.coin)"
                            class="provider-item item-price d-flex align-center"
                            ref="refBoxDeno"
                        >
                            <div class="row price-info">
                                <font-awesome-icon :icon="['fas', 'coins']" />&nbsp;{{ den.coin }} xu
                            </div>
                            <div class="row price-discount">
                                <div class="discount-text">Tiền nạp:</div>
                                <div class="discount-value">{{ den.price }}đ</div>
                            </div>
                            <!---->
                        </div>
                        <!---->
                    </div>
                </template>
            </div>
            <ol>
                <li>Vào ứng dụng ngân hàng của bạn trên điện thoại và chọn chức năng quét mã QR trong đó</li>
                <li>Quét mã QR bên dưới</li>
                <li>Nhập số tiền bạn muốn chuyển ứng với số xu bạn muốn nạp</li>
                <li>Nội dung chuyển tiền: VIP be69z</li>
                <li>
                    <div>
                        Sau khi hoàn tất quá trình chuyển tiền (tên người nhận tiền là NGUYEN QUANG HUY), bạn vui lòng
                        chọn
                        <span style="color: #002bff; font-weight: 700">khung mệnh giá mình đã chuyển bên trên</span> và
                        nhấn vào nút <span style="color: #50b83c; font-weight: 700">"Hoàn tất"</span> ở bên dưới. Sau
                        khoảng 1, 2 phút, tài khoản bạn trên web sẽ được cộng tiền.
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center">
                        <img src="../../assets/img/QR_VCB.png" style="width: 200px; height: 200px" />
                    </div>
                </li>
                <li>
                    Nếu bạn không quét mã được, vui lòng chuyển tiền theo thông tin sau:
                    <ul style="list-style: circle; margin-left: 20px">
                        <li>Ngân hàng TMCP Ngoại thương Việt Nam (Vietcombank)</li>
                        <li>Số tài khoản: <span style="font-weight: 700">0341007208400</span></li>
                        <li>Tên người nhận: <span style="font-weight: 700">NGUYEN QUANG HUY</span></li>
                        <li>
                            Nội dung chuyển tiền:
                            <span style="font-weight: 700; color: red">VIP be69z</span>
                        </li>
                    </ul>
                </li>
            </ol>
            <div class="btn-group" style="padding-top: 0 !important; display: flex; justify-content: space-between">
                <MButton
                    @click="btnCloseOnClick"
                    :class="{ 'btn-custom-default': true, 'btn-bg': true }"
                    label="Hủy"
                ></MButton>
                <div class="btn-left">
                    <MButton
                        @click="btnRecharge"
                        :class="{ 'btn-custom-default': true, 'btn-bg': true }"
                        label="Hoàn tất"
                        ref="btnSave"
                    ></MButton>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    name: 'RechargeMoney',
    methods: {
        btnCloseOnClick() {
            this.$emit('onClose');
        },
        btnSelectDenomination(event, valueCoin) {
            // this.addClassSelected = 0

            const boxDeno = this.$refs.refBoxDeno;
            const main = event.currentTarget;
            const className = 'selected';
            let myContent = null;

            // let tdContent = null;
            boxDeno.forEach((item) => {
                // Sử dụng parentNode vì phần tử được nhấp nằm trong div của chính nó.

                if (main.parentNode.contains(item)) {
                    myContent = item;
                } else {
                    item.classList.remove(className);
                }
            });

            // Chuyển đổi ẩn hiện khi được nhấp
            if (myContent) myContent.classList.toggle(className);

            this.orderInfor.recharge = valueCoin;
        },

        async btnRecharge() {
            var me = this;
            me.orderInfor.orderName = 'Yêu cầu nạp tiền';
            me.orderInfor.userId = this.$route.params.id;
            me.orderInfor.movieId = null;
            await axios
                .post(`${this.$MResource.API}/OrderUsers`, me.orderInfor)
                .then(() => {
                    this.$MToastMessage({
                        titleToast: this.$MResource.VI.TOAST.TITLE_SUCCESS,
                        messageToast: 'Yêu cầu nạp tiền đã được tiếp nhận',
                        showToastMessage: true,
                        typeToast: 'successToast',
                    });

                    const storage = localStorage.getItem('userInfor');
                    if (storage !== null) {
                        this.userInformation = JSON.parse(storage);
                        console.log('du lieu da duoc goi den day: ', this.userInformation);
                        if (this.userInformation.fullName == '' || this.userInformation.fullName == null) {
                            this.userInformation.fullName = 'Tài khoản chưa cập nhật tên đầy đủ';
                        }
                        axios
                            .post(
                                `${this.$MResource.API}/OrderUsers/SendEmail?subjectMess=${this.orderInfor.orderName}&accountName=${this.userInformation.username}&customerName=${this.userInformation.fullName}&moneyRecharge=${this.orderInfor.recharge}`,
                            )
                            .then(() => {})
                            .catch((err) => {
                                console.log(err);
                            });
                    }

                    me.$emit('onClose');
                    // location.reload();
                })
                .catch((err) => {
                    let response = err.response;
                    switch (response.status) {
                        case 500:
                            if (response.data['errorCode'] === 5) {
                                this.$MToastMessage({
                                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                    messageToast: response.data['userMsg'],
                                    showToastMessage: true,
                                    typeToast: 'errorToast',
                                });
                            } else {
                                this.$MToastMessage({
                                    titleToast: this.$MResource.VI.TOAST.TITLE_ERROR,
                                    messageToast: response.data['devMsg'],
                                    showToastMessage: true,
                                    typeToast: 'errorToast',
                                });
                            }
                            break;
                        case 400:
                            var userMsg = response.data['moreInfo'];
                            userMsg.forEach((element) => {
                                this.showDialogWarning = true;
                                this.textWarning = element;
                            });

                            break;
                        default:
                            break;
                    }
                });
        },
    },
    data() {
        return {
            orderInfor: { orderName: '', recharge: 0, userId: '', movieId: '' },
            denominationList: [
                {
                    coin: 100,
                    price: '10.000',
                },
                {
                    coin: 200,
                    price: '20.000',
                },
                {
                    coin: 500,
                    price: '50.000',
                },
                {
                    coin: 1000,
                    price: '100.000',
                },
            ],

            userInformation: {},
        };
    },
};
</script>

<style scoped>
.modal-instruction {
    position: fixed;
    background-color: rgba(0, 0, 0, 0.4);
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 99999;
}

.instructure-recharge {
    width: 70%;
    margin: 0 auto;
    padding: 20px;
    border-radius: 10px;
    background-color: #fff;
    max-height: 95%;
}

.instructure-recharge h2 {
    margin-bottom: 12px;
}
.amount {
    border: 2px solid red;
    border-radius: 10px;
    padding: 10px;
    font-size: 20px;
}

.denomination {
    display: flex;
    flex-direction: row;
}

.provider-item.item-price {
    display: block !important;
    padding: 10px 10px 2px;
}

.provider-item {
    display: block;
    border: 1px solid #d3d3d3;
    border-radius: 8px;
    padding: 4px;
    min-height: 60px !important;
    max-width: 130px !important;
    cursor: pointer;
    position: relative;
}

.provider-item.item-price.selected {
    display: block !important;
    padding: 9px 9px 1px;
}

.provider-item.selected {
    border: 2px solid #f25922;
    padding: 3px;
}

.provider-item.item-price {
    display: block !important;
    padding: 10px 10px 2px;
}

.provider-item.item-price.selected .price-info {
    color: #f25922 !important;
    padding-bottom: 15px;
    padding-top: 15px;
}

.provider-item .price-info {
    color: #0a0a0a !important;
    display: block;
    padding-bottom: 15px;
    padding-top: 15px;
    font-size: 18px;
    text-align: center;
    font-weight: 500;
}

.provider-item .price-discount {
    display: flex;
    border-top: 1px dotted #333;
    padding-top: 4px;
    margin: 0;
}

.provider-item .price-discount .discount-text {
    float: left;
    font-size: 12px;
    color: #0a0a0a;
    width: 49%;
}

.provider-item .price-discount .discount-value {
    float: right;
    font-size: 13px;
    color: #002bff;
    width: 49%;
    text-align: right;
    font-weight: 500;
}
</style>
