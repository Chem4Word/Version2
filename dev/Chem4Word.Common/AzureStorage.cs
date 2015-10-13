// Created by Mike Williams - 22/09/2015
// 
// -----------------------------------------------------------------------
//   Copyright (c) 2015, The Outercurve Foundation.  
//   This software is released under the Apache License, Version 2.0. 
//   The license and further copyright text can be found in the file LICENSE.TXT at
//   the root directory of the distribution.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Diagnostics;

namespace Chem4Word.Common
{
    public class AzureStorage
    {
        // http://gauravmantri.com/2012/11/17/storage-client-library-2-0-migrating-table-storage-code/
        // http://www.britishdeveloper.co.uk/2012/11/upgrading-azure-storage-client-library.html
        // http://blog.liamcavanagh.com/2011/11/how-to-sort-azure-table-store-results-chronologically/
        // https://convective.wordpress.com/2013/11/03/queries-in-the-windows-azure-storage-client-library-v2-1/

        private static string accountName = "c4wtelemetry";
        private static string accountKey = "60KaUHLXdj9sh4x3h1qgBRS3BMM2UbAAoeMChrp9xMBPutpB27feYMbUfcsqZb1PTu0rkJhruhkUD1ytuWjUoQ==";
        private static string tableName = "Logging";

        public AzureStorage()
        {
            try
            {
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentialsAccountAndKey(accountName, accountKey), true);
                CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
                cloudTableClient.CreateTableIfNotExist(tableName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
        }

        public bool WriteMessage(MessageEntity messageEntity)
        {
            bool success = true;

            try
            {
                Debug.WriteLine(messageEntity.Operation);
                Debug.WriteLine("  " + messageEntity.Level + " - " + messageEntity.Message);

                messageEntity.PartitionKey = "Chem4Word";
                messageEntity.RowKey = string.Format("{0:D19}", DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks);

                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentialsAccountAndKey(accountName, accountKey), true);
                CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();

                TableServiceContext serviceContext = cloudTableClient.GetDataServiceContext();
                serviceContext.AddObject(tableName, messageEntity);
                serviceContext.SaveChanges();
                //serviceContext.BeginSaveChangesWithRetries(SaveChangesOptions.Batch,
                //    (asyncResult => serviceContext.EndSaveChangesWithRetries(asyncResult)), null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
                success = false;
            }

            return success;
        }
    }

    public class MessageEntity : TableServiceEntity
    {
        public MessageEntity()
        {
        }

        public string MachineId { get; set; }
        public string Operation { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
    }

}
