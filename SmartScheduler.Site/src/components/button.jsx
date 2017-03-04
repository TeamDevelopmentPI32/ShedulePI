import React from 'react';

const ButtonComponent = ({ 
    type,
    className,
    title,
    onClick,
    disabled
}) => (
    <button 
        type={ type } 
        className={ className }
        onClick={ onClick }
        disabled={ disabled }>

        { title }
        
    </button>
);

// Button propTypes (requirements)
ButtonComponent.propTypes = {
    type: React.PropTypes.string.isRequired,
    className: React.PropTypes.string.isRequired,
    title: React.PropTypes.string.isRequired,
    onClick: React.PropTypes.func
}

export default ButtonComponent;