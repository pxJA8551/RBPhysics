using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RBPhys
{
    public abstract class RBTrajectoryAlternateValidator
    {
        public abstract bool Validate();

        public Guid ValidatorSrcGuid { get { return _validatorSrcGuid; } }
        readonly Guid _validatorSrcGuid;

        public RBTrajectoryAlternateValidator(Guid validatorSrcGuid)
        {
            _validatorSrcGuid = validatorSrcGuid;
        }

        public bool ValidateSrc(Guid validatorSrcGuid)
        {
            return _validatorSrcGuid != Guid.Empty && _validatorSrcGuid == validatorSrcGuid;
        }
    }
}