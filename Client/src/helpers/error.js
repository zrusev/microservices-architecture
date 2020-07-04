const Handler = alert => {
    const error = {
        type: 'InternalError',
        message: 'Something went wrong'
    }

    if(alert.message) {
        if (typeof alert.message?.message === 'string' || alert.message?.message instanceof String) {
            error.type = 'InternalError';
            error.message = alert.message.message;
        }

        if ((typeof alert.message === 'object' || alert.message instanceof Object) 
            && Array.isArray(Object.values(alert.message)[0])) {
                error.type = Object.keys(alert.message)[0];
                error.message = Object.values(alert.message)[0].join(", ");
        }
    }

    return error;
}

export default Handler;