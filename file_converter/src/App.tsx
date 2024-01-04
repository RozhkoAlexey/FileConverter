import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Card } from "react-bootstrap";
import {
  UploadFileComponent,
  HeaderComponent,
  ListFileComponent,
} from "./components";
import { useAppDispatch, useAppSelector } from "./app-store/hooks";
import { useEffect } from "react";
import { authAction } from "./app-store/slices/auth-slice";
import { v4 as uuidv4 } from "uuid";
import { GetFileStatus } from "./api-services/files-api-service";
import { ItemFileConvertStatus } from "./types/item-file";
import { filesActions } from "./app-store/slices/files-slice";

const App = () => {
  const dispatch = useAppDispatch();
  const authKey = useAppSelector(({ auth }) => auth.key);
  const files = useAppSelector(({ files }) => files.items);

  useEffect(() => {
    if (authKey) return;

    dispatch(authAction.auth(uuidv4()));
  }, [dispatch, authKey]);

  useEffect(() => {
    const interval = setInterval(() => {
      const filesForCheck = files.filter((x) => !x.isConverted && !x.isError);

      for (const item of filesForCheck) {
        GetFileStatus(item.fileId, authKey).then((data) => {
          if (data && data.status === ItemFileConvertStatus.Completed) {
            dispatch(filesActions.setConverted(data));
          }
        });
      }
    }, 2000);
    return () => {
      clearInterval(interval);
    };
  }, [files, dispatch, authKey]);

  return (
    <div className="row">
      <div className="col-12">
        <Card>
          <Card.Header>
            <HeaderComponent />
          </Card.Header>
          <Card.Body>
            <ListFileComponent />
          </Card.Body>
          <Card.Footer>
            <UploadFileComponent />
          </Card.Footer>
        </Card>
      </div>
    </div>
  );
};

export default App;
