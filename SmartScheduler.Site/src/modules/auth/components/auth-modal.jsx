import React from 'react';
import { default as Modal } from '../../../components/modal';
import { default as AuthForm } from './auth-form';

const AuthModalComponent = ({ 
    isOpen,
    onRequestClose,
    name,
    title
}) => (
    <Modal
        isOpen={ isOpen } 
        closeModal={ onRequestClose } 
        name={ name }
        title={ title }>

        <AuthForm handleClose={ onRequestClose } />
    </Modal>
);

export default AuthModalComponent;
    