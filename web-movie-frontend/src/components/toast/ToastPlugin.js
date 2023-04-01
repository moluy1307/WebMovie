import MToastMessage from './MToastMessage.vue';
import { createApp } from 'vue';
const install = (app) => {
    let container;
    let toastApp;

    const toast = (titleToast, messageToast, showToastMessage, typeToast) => {
        if (typeof titleToast === 'string') {
            titleToast = { titleToast };
        }
        if (typeof messageToast === 'string') {
            messageToast = { messageToast };
        }
        if (typeof showToastMessage === 'boolean') {
            showToastMessage = { showToastMessage };
        }
        if (typeof typeToast === 'string') {
            typeToast = { typeToast };
        }

        const props = { ...titleToast, ...messageToast, ...showToastMessage, ...typeToast };

        container = document.createElement('div');
        container.setAttribute('id', 'ToastPlug');
        document.body.appendChild(container);
        toastApp = createApp(MToastMessage, props);
        toastApp.mount(container);
    };
    app.config.globalProperties.$MToastMessage = toast;
};
export default install;
