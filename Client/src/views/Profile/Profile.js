import React from 'react';
import { useSelector } from 'react-redux';

export const Profile = () => {
    const customer = useSelector(state => state.customer.customer);

    return (
        <div>
            {
                customer.firstName
            }
        </div>
    )
}