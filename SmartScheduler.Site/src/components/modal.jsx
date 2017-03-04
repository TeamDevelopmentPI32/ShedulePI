import React from 'react';
import Modal from 'react-modal';

const ModalComponent = ({ 
    isOpen, 
    closeModal, 
    name, 
    title, 
    children 
}) => (
    <Modal
        className="b-modal"
        overlayClassName="b-modal-overlay"
        isOpen={ isOpen } 
        closeTimeoutMS={ 200 }
        onRequestClose={ closeModal } 
        contentLabel={ name }>

        <header className="b-modal__header">
            <h3 className="b-modal__title">{ title }</h3>
            
            <button onClick={ closeModal } className="b-modal__close-button">
                <i className="material-icons">close</i>
            </button>
        </header>
        
        <section className="b-modal__body">
            { children }
        </section>
    </Modal>
);

ModalComponent.propTypes = {
    isOpen: React.PropTypes.bool.isRequired,
    closeModal: React.PropTypes.func.isRequired,
    name: React.PropTypes.string.isRequired,
    title: React.PropTypes.string.isRequired
};

export default ModalComponent;