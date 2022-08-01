pipeline {
	agent any
	
	environment {
		AWS_ACCOUNT_ID="274353818375"
		AWS_DEFAULT_REGION="us-east-1"
		CLUSTER_NAME="default"
		SERVICE_NAME="classifieds-container-service"
		TASK_DEFINITION_NAME="first-run-task-definition:1"
		DESIRED_COUNT="1"
		IMAGE_REPO_NAME="classifiedsblazor"
		IMAGE_TAG="${env.BUILD_ID}"
		REPOSITORY_URI="${AWS_ACCOUNT_ID}.dkr.ecr.us-east-1.amazonaws.com/${IMAGE_REPO_NAME}"
		registryCredential = "classifieds-user"
        FRONTEND_PATH = ""
        BACKEND_PATH = ""
    }
   
    stages {

    // Tests
    stage('Unit Tests') {
      steps{
        script {
          //sh 'npm install'
	  //sh 'npm test -- --watchAll=false'
      echo 'unit tests'
        }
      }
    }
        
    // Building Docker images
    stage('Building image') {
      steps{
        script {
        //stop old containers
            //sh 'docker-compose –f docker-compose.yml down -v'
            sh 'docker-compose build'
            echo 'Docker-compose-build Build Image Completed'   

          
        }
      }
    }
   
    // Uploading Docker images into AWS ECR
    stage('Pushing to ECR') {
     steps{  
         script {
			docker.withRegistry("https://" + REPOSITORY_URI, "ecr:${AWS_DEFAULT_REGION}:" + registryCredential) {
                        sh 'docker push ${REPOSITORY_URI}-be'
                        sh 'docker push ${REPOSITORY_URI}-fe'


                        echo 'finished pushing to ECR...'
                	}
         }
        }
      }
      
    stage('Deploy') {
     steps{
            withAWS(credentials: registryCredential, region: "${AWS_DEFAULT_REGION}") {
                script {
			        sh 'docker-compose up'

                    echo 'finished deploying containers...'
                }
            }
        }      
      }
    }
}