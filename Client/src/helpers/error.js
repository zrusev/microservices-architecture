const Handler = alert => {
    const defaultMessage = {
        type: alert.type || 'InternalError',
        severity: alert.severity || 'info',
        message: 'Something went wrong'
    }

    if(alert.message) {
        if (typeof alert.message?.message === 'string' || alert.message?.message instanceof String) {
            defaultMessage.message = alert.message.message;
        }

        if ((typeof alert.message === 'object' || alert.message instanceof Object)
            && Array.isArray(Object.values(alert.message)[0])) {
                defaultMessage.type = Object.keys(alert.message)[0];
                defaultMessage.severity = 'error';
                defaultMessage.message = Object.values(alert.message)[0].join(", ");
        }
    }

    return defaultMessage;
}

export default Handler;