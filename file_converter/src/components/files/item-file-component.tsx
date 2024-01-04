import styles from "./style.module.scss";
import { FC } from "react";
import * as Icon from "react-bootstrap-icons";
import Spinner from "react-bootstrap/Spinner";
import { useAppDispatch, useAppSelector } from "../../app-store/hooks";
import { filesActions } from "../../app-store/slices/files-slice";
import { DeleteFile, DownloadFile } from "../../api-services/files-api-service";
import { downloadFile } from "../../utils/download-file";

type Props = {
  fileName: string;
  fileId: string;
  isConverted: boolean;
  isError: boolean;
  resultFileName?: string;
};

export const ItemFileComponent: FC<Props> = ({
  fileName,
  fileId,
  isConverted,
  isError,
  resultFileName,
}) => {
  const appDispatch = useAppDispatch();
  const authKey = useAppSelector((x) => x.auth.key);

  const handleTrash = () => {
    DeleteFile(fileId, authKey).finally(() => {
      appDispatch(filesActions.remove(fileId));
    });
  };

  const handleDownloadFile = () => {
    DownloadFile(fileId, authKey).then((data) => {
      downloadFile(data, resultFileName!, "application/pdf");
    });
  };

  return (
    <div
      className={`${styles.fileItemContainer} d-flex justify-content-between`}
    >
      <div className="fs-6">{fileName}</div>
      <div>
        {isConverted && !isError && (
          <>
            <Icon.Download
              className="text-success mx-3"
              onClick={handleDownloadFile}
            />
            <Icon.Trash className="text-danger" onClick={handleTrash} />
          </>
        )}
        {!isConverted && !isError && (
          <Spinner
            animation="border"
            role="status"
            variant="primary"
            size="sm"
            className=""
          >
            <span className="visually-hidden">Loading...</span>
          </Spinner>
        )}

        {isError && (
          <>
            <Icon.ExclamationTriangleFill className="text-warning mx-3" />
            <Icon.Trash className="text-danger" onClick={handleTrash} />
          </>
        )}
      </div>
    </div>
  );
};
