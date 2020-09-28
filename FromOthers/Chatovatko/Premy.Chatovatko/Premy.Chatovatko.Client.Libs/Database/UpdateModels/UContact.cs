using Premy.Chatovatko.Client.Libs.Database.JsonModels;
using Premy.Chatovatko.Client.Libs.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.UpdateModels
{
    public class UContact : IUpdateModel
    {

        private readonly byte[] oldSendAesKey;
        private readonly byte[] oldReceiveAesKey;

        public UContact(Contacts detail)
        {
            NickName = detail.NickName;
            PublicId = detail.PublicId;
            AlarmPermission = detail.AlarmPermission == 1;
            ReceiveAesKey = detail.ReceiveAesKey;
            SendAesKey = detail.SendAesKey;
            PublicCertificate = detail.PublicCertificate;
            Trusted = detail.Trusted == 1;
            UserName = detail.UserName;

            oldSendAesKey = detail.SendAesKey;
            oldReceiveAesKey = detail.ReceiveAesKey;
        }

        public long PublicId { get; }
        public string UserName { get; }
        public string PublicCertificate { get; }
        public bool Trusted { get; set; }
        public byte[] SendAesKey { get; set; }
        public byte[] ReceiveAesKey { get; set; }
        public bool AlarmPermission { get; set; }
        public string NickName { get; set; }

        public UpdateModelTypes GetModelType()
        {
            return UpdateModelTypes.CONTACT;
        }

        private void VerifyAesKeyChange()
        {
            if(oldReceiveAesKey != null && oldReceiveAesKey != ReceiveAesKey)
            {
                throw new Exception("It isn't possible to update receive Aes key.");
            }

            if (oldSendAesKey != null && oldSendAesKey != SendAesKey)
            {
                throw new Exception("It isn't possible to update send Aes key.");
            }
        }

        public JsonCapsula GetSelfUpdate()
        {
            VerifyAesKeyChange();
            return new JsonCapsula(new JContact()
            {
                AlarmPermission = this.AlarmPermission,
                PublicId = this.PublicId,
                UserName = this.UserName,
                PublicCertificate = this.PublicCertificate,
                Trusted = this.Trusted,
                SendAesKey = this.SendAesKey,
                ReceiveAesKey = this.ReceiveAesKey,
                NickName = this.NickName
            });
        }
    

        public JsonCapsula GetRecepientUpdate()
        {
            VerifyAesKeyChange();
            return null;
        }
    }
}
