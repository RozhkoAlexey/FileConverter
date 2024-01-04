import { useAppSelector } from "../../app-store/hooks";
import { ItemFileComponent } from "./item-file-component";

export const ListFileComponent = () => {
  const files = useAppSelector((x) => x.files.items);

  return (
    <div className="container">
      {files?.map((x) => (
        <div key={x.fileId} className="row d-flex justify-content-center">
          <ItemFileComponent
            fileId={x.fileId}
            fileName={x.fileName}
            isConverted={x.isConverted}
            isError={x.isError}
            resultFileName={x.resultFileName}
          />
        </div>
      ))}
    </div>
  );
};
